////////////////////////////////////////////////////////////////////////////////////////////////////////
// LLBLGen Pro LINQPad driver is (c) 2002-2012 Solutions Design. All rights reserved.
// http://www.llblgen.com
////////////////////////////////////////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c)2002-2012 Solutions Design. All rights reserved.
// http://www.llblgen.com
// 
// The LLBLGen Pro LINQPad driver sourcecode is released under the following license (BSD2):
// ----------------------------------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////
// Special thanks to:
//		- Jeremy Thomas
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQPad.Extensibility.DataContext;
using System.Reflection;
using SD.LLBLGen.Pro.ORMSupportClasses;
using LinqExpression = System.Linq.Expressions.Expression;
using System.ComponentModel;
using SD.LLBLGen.Pro.LinqSupportClasses;
using LINQPad;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Class which produces the list of ExplorerItem objects which represent the schema of the selected entity assembly.
	/// </summary>
	internal class SchemaBuilder
	{
		#region Class Member Declarations
		private IConnectionInfo _cxInfo;
		private Type _linqMetaDataType;
		private Delegate _callGetEntityFactoryDelegate;
		private Dictionary<string, EntityTypeSchemaData> _entityTypeSchemaDataPerEntityName;
		private Dictionary<Type, EntityTypeSchemaData> _entityTypeSchemaDataPerType;

		/// <summary>
		/// Lock object for factory delegate creator method. 
		/// </summary>
		private static object _semaphore = new object();
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="SchemaBuilder"/> class.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="linqMetaDataType">Type of the linq meta data.</param>
		public SchemaBuilder(IConnectionInfo cxInfo, Type linqMetaDataType)
		{
			_cxInfo = cxInfo;
			_linqMetaDataType = linqMetaDataType;
		}


		/// <summary>
		/// Gets the schema.
		/// </summary>
		/// <returns></returns>
		internal List<ExplorerItem> GetSchema()
		{
			VerifyEntityAssemblyVersion();
			// We're only interested in the DataSource(2)<TEntity> returning properties. 
			var queryableProperties = _linqMetaDataType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
																.Where(p => typeof(IQueryable).IsAssignableFrom(p.PropertyType));
			_entityTypeSchemaDataPerEntityName = new Dictionary<string, EntityTypeSchemaData>();
			_entityTypeSchemaDataPerType = new Dictionary<Type, EntityTypeSchemaData>();

			// first we discover all entity types.
			foreach(var property in queryableProperties)
			{
				if((!property.PropertyType.IsGenericType) && !typeof(IQueryable).IsAssignableFrom(property.PropertyType))
				{
					continue;
				}
				var entityType = property.PropertyType.GetGenericArguments()[0];
				ProduceEntityFactoryDelegate(entityType);
				var factory = GetFactory(entityType);
				if(factory == null)
				{
					continue;
				}
				var dummyInstance = factory.Create();
				var entitySchemaData = new EntityTypeSchemaData()
				{
					EntityType = entityType,
					Factory = factory,
					InheritanceInfo = dummyInstance.GetInheritanceInfo(),
					LinqMetaDataProperty = property
				};
				_entityTypeSchemaDataPerEntityName[dummyInstance.LLBLGenProEntityName] = entitySchemaData;
				_entityTypeSchemaDataPerType[entityType] = entitySchemaData;
			}

			// traverse all entity type schema data objects we've created and recurse over the types to build all property data
			foreach(var entityName in _entityTypeSchemaDataPerEntityName.Keys)
			{
				AddEntityExplorerItem(entityName);
			}

			// second iteration, we discover all navigators. Because all entity types are known we can simply use the lookups
			foreach(var entityStateData in _entityTypeSchemaDataPerEntityName.Values.Distinct())
			{
				DiscoverEntityNavigators(entityStateData);
			}
			return _entityTypeSchemaDataPerType.Values.Select(v => v.RelatedExplorerItem).ToList();
		}


		/// <summary>
		/// Verifies the version of the entity assembly, whether it's of a version this driver can work with.
		/// </summary>
		private void VerifyEntityAssemblyVersion()
		{
			if(_linqMetaDataType == null)
			{
				throw new InvalidOperationException("No ILinqMetaData type found.");
			}


			var ormSupportClassesAssemblyName = _linqMetaDataType.Assembly.GetReferencedAssemblies().FirstOrDefault(an => an.Name=="SD.LLBLGen.Pro.ORMSupportClasses");
            if(ormSupportClassesAssemblyName == null) throw new InvalidOperationException($"The assembly '{CxInfoHelper.GetEntityAssemblyFilename(_cxInfo, CxInfoHelper.GetTemplateGroup(_cxInfo))}' is not found.");
            if (ormSupportClassesAssemblyName.Version.Major != Constants.MajorVersion || ormSupportClassesAssemblyName.Version.Minor != Constants.MinorVersion) {
                throw new InvalidOperationException($"The assembly '{CxInfoHelper.GetEntityAssemblyFilename(_cxInfo, CxInfoHelper.GetTemplateGroup(_cxInfo))}' is compiled against version {ormSupportClassesAssemblyName.Version.Major}.{ormSupportClassesAssemblyName.Version.Minor} and not the required LLBLGen Pro Runtime Framework v{Constants.MajorVersion}.{Constants.MinorVersion}.");
            }
		}
		

		/// <summary>
		/// Discovers the entity navigators.
		/// </summary>
		/// <param name="entitySchemaData">The entity schema data.</param>
		private void DiscoverEntityNavigators(EntityTypeSchemaData entitySchemaData)
		{
			var entityType = entitySchemaData.EntityType;
			if(entitySchemaData.Factory == null)
			{
				throw new InvalidOperationException(string.Format("Factory is null in entitySchemaData for entity '{0}'", entitySchemaData.EntityType.FullName));
			}
			var dummyInstance = entitySchemaData.Factory.Create();
			var relationsWithMappedFields = dummyInstance.GetAllRelations().Where(r => !string.IsNullOrEmpty(r.MappedFieldName) && !r.IsHierarchyRelation);
			var relationPerMappedFieldName = relationsWithMappedFields.ToDictionary(r => r.MappedFieldName);

			var properties = TypeDescriptor.GetProperties(entityType).Cast<PropertyDescriptor>();
			var inheritedProperties = new HashSet<PropertyDescriptor>(DetermineInheritedProperties(entitySchemaData.EntityType, properties));
			var childrenCollection = entitySchemaData.RelatedExplorerItem.Children;
			foreach(PropertyDescriptor property in properties)
			{
				if(!(typeof(IEntityCore).IsAssignableFrom(property.PropertyType) || typeof(IEntityCollectionCore).IsAssignableFrom(property.PropertyType)))
				{
					// not an entity navigator, 
					continue;
				}
				string suffix = string.Empty;
				bool inherited = false;
				if(inheritedProperties.Contains(property))
				{
					suffix = string.Format(" (Inherited from '{0}')", property.ComponentType.Name.Replace("Entity", ""));
					inherited=true;
				}
				// relationMapped can be null, in the case of a m:n navigator.
				IEntityRelation relationMapped = null;
				relationPerMappedFieldName.TryGetValue(property.Name, out relationMapped);
				if(typeof(IEntityCore).IsAssignableFrom(property.PropertyType))
				{
					var relatedEntitySchemaData = GetEntitySchemaDataForType(property.PropertyType);
					// single entity navigator, or property added manually. 
					if(relationMapped == null)
					{
						// property added manually, ignore.
						continue;
					}
					var icon = relationMapped.TypeOfRelation == RelationType.ManyToOne ? ExplorerIcon.ManyToOne : ExplorerIcon.OneToOne;
					if(inherited)
					{
						icon=ExplorerIcon.Inherited;
					}
					childrenCollection.Add(new ExplorerItem(property.Name + suffix, ExplorerItemKind.ReferenceLink, icon)
								{
									DragText = property.Name,
									HyperlinkTarget = relatedEntitySchemaData == null ? null : relatedEntitySchemaData.RelatedExplorerItem
								});
					continue;
				}
				if(typeof(IEntityCollectionCore).IsAssignableFrom(property.PropertyType))
				{
					// collection navigator. We have to determine which entity type is returned from the collection. 
					// First, check if there's a TypeContainedAttribute on the property. If so, use the type in the attribute. If not,
					// try to determine the type using the linq utils method. 
					Type containedType = null;
					var typeContainedAttribute = property.Attributes[typeof(TypeContainedAttribute)] as TypeContainedAttribute;
					if((typeContainedAttribute != null) && typeContainedAttribute.TypeContainedInCollection != null)
					{
						containedType = typeContainedAttribute.TypeContainedInCollection;
					}
					else
					{
						containedType = LinqUtils.DetermineEntityTypeFromEntityCollectionType(property.PropertyType);
					}
					var relatedEntitySchemaData = GetEntitySchemaDataForType(containedType);
					var icon = relationMapped == null ? ExplorerIcon.ManyToMany : ExplorerIcon.OneToMany;
					if(inherited)
					{
						icon = ExplorerIcon.Inherited;
					}
					childrenCollection.Add(new ExplorerItem(property.Name + suffix, ExplorerItemKind.CollectionLink, icon)
								{
									DragText = property.Name,
									HyperlinkTarget = relatedEntitySchemaData == null ? null : relatedEntitySchemaData.RelatedExplorerItem
								});
				}
			}
		}


		/// <summary>
		/// Gets the type of the entity schema data for.
		/// </summary>
		/// <param name="entityType">Type of the entity.</param>
		/// <returns></returns>
		private EntityTypeSchemaData GetEntitySchemaDataForType(Type entityType)
		{
			EntityTypeSchemaData toReturn = null;
			_entityTypeSchemaDataPerType.TryGetValue(entityType, out toReturn);
			return toReturn;
		}


		/// <summary>
		/// Adds the explorer item for the entity with the name specified to the entity's schemadata object. If it's already present, 
		/// it simply returns that explorer item. 
		/// </summary>
		/// <param name="entityName">Name of the entity.</param>
		private void AddEntityExplorerItem(string entityName)
		{
			EntityTypeSchemaData entitySchemaData = null;
			if(!_entityTypeSchemaDataPerEntityName.TryGetValue(entityName, out entitySchemaData))
			{
				return;
			}
			if(entitySchemaData.RelatedExplorerItem != null)
			{
				return;
			}
			// not yet created. 
			var entityNameForElement = entityName.Replace("Entity", "");
			var suffix = string.Empty;
			if(!string.IsNullOrEmpty(entitySchemaData.SuperTypeName))
			{
				suffix = string.Format(" (Sub-type of '{0}')", entitySchemaData.SuperTypeName.Replace("Entity", ""));
			}
			entitySchemaData.RelatedExplorerItem = new ExplorerItem(entityNameForElement + suffix, ExplorerItemKind.QueryableObject, ExplorerIcon.Table)
													{
														DragText = entityNameForElement,
														IsEnumerable = true,
														Children = new List<ExplorerItem>()
													};

			var dummyInstance = entitySchemaData.Factory.Create();
			DiscoverEntityFields(dummyInstance, entitySchemaData);
		}


		/// <summary>
		/// Discovers the entity fields for the entity type specified.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="entityType">Type of the entity.</param>
		/// <param name="customState">State of the custom.</param>
		private void DiscoverEntityFields(IEntityCore instance, EntityTypeSchemaData entitySchemaData)
		{
			var instanceFieldsToTravese = instance.Fields
							.Where(f =>!f.IsPrimaryKey || (f.IsPrimaryKey && (f.ActualContainingObjectName==f.ContainingObjectName)))
							.ToDictionary(f=>f.Name);
			var allProperties = TypeDescriptor.GetProperties(entitySchemaData.EntityType).Cast<PropertyDescriptor>();
			var propertiesToTraverse = allProperties.Where(p => instanceFieldsToTravese.ContainsKey(p.Name));
			var childrenCollection = entitySchemaData.RelatedExplorerItem.Children;
			List<ExplorerItem> toAdd = new List<ExplorerItem>();
			foreach(PropertyDescriptor property in propertiesToTraverse)
			{
				var field = instance.Fields[property.Name];
				bool isPK = false;
				bool isFK = false;
				int fieldIndex = -1;
				if(field == null)
				{
					// other property
					if(typeof(IEntityCore).IsAssignableFrom(property.PropertyType) || typeof(IEntityCollectionCore).IsAssignableFrom(property.PropertyType))
					{
						// entity navigator, done later
						continue;
					}
					else
					{
						if(!property.IsBrowsable)
						{
							continue;
						}
					}
				}
				else
				{
					isPK = field.IsPrimaryKey;
					isFK = field.IsForeignKey;
					fieldIndex = field.FieldIndex;
				}
				string propertyText = property.Name;
				if(isFK)
				{
					propertyText+=" (FK)";
				}
				var icon = isPK ? ExplorerIcon.Key : ExplorerIcon.Column;
				if(field.ActualContainingObjectName != field.ContainingObjectName)
				{
					// inherited field.
					propertyText += string.Format(" (Inherited from '{0}')", field.ContainingObjectName.Replace("Entity", ""));
					icon = ExplorerIcon.Inherited;
				}
				toAdd.Add(new ExplorerItem(propertyText, ExplorerItemKind.Property, icon)
					{
						DragText = property.Name,
						Tag = fieldIndex
					});
			}
			childrenCollection.AddRange(toAdd.OrderBy(i => (int)i.Tag).ThenBy(i => i.Text));
		}


		/// <summary>
		/// Determines the inherited properties. This method only marks a property as inherited if it belongs to another entity or to commonentitybase or
		/// classes up in the entity hierarchy.
		/// </summary>
		/// <param name="entityType">Type of the entity.</param>
		/// <param name="allProperties">All properties.</param>
		/// <returns>enumerable of property descriptors which are truly inherited from other entities.</returns>
		private IEnumerable<PropertyDescriptor> DetermineInheritedProperties(Type entityType, IEnumerable<PropertyDescriptor> allProperties)
		{
			Type baseType = entityType.BaseType;
			bool includeBaseTypeProperties = false;
			if(typeof(IEntity).IsAssignableFrom(entityType))
			{
				// selfservicing
				// check for 2-class scenario
				includeBaseTypeProperties = baseType.Name.StartsWith(entityType.Name);
			}
			else
			{
				// adapter.
				// check for 2-class scenario
				includeBaseTypeProperties = entityType.Name.Equals("My" + baseType.Name);
			}
			if(includeBaseTypeProperties)
			{
				return allProperties.Where(p => p.ComponentType != entityType && p.ComponentType != baseType);
			}
			else
			{
				return allProperties.Where(p => p.ComponentType != entityType);
			}
		}
		

		/// <summary>
		/// Gets the factory for the entity with the type specified.
		/// </summary>
		/// <param name="elementType">Type of the element.</param>
		/// <returns>the factory for the element specified, or null if not found</returns>
		private IEntityFactoryCore GetFactory(Type elementType)
		{
			if(_callGetEntityFactoryDelegate == null)
			{
				return null;
			}
			return (IEntityFactoryCore)_callGetEntityFactoryDelegate.DynamicInvoke(elementType);
		}


		/// <summary>
		/// Produces the entity factory delegate and sets the 
		/// </summary>
		/// <param name="elementType">Type of the element.</param>
		private void ProduceEntityFactoryDelegate(Type elementType)
		{
			if(typeof(IEntityCore).IsAssignableFrom(elementType) && (_callGetEntityFactoryDelegate == null))
			{
				lock(_semaphore)
				{
					if(_callGetEntityFactoryDelegate == null)
					{
						// create the delegate for invoking the entity factory factory.
						string rootNamespace = elementType.FullName.Substring(0, elementType.FullName.Length - (elementType.Name.Length + ".EntityClasses".Length) - 1);
						string generalEntityFactoryFactoryTypeName = rootNamespace + ".FactoryClasses.EntityFactoryFactory";
						Type generalEntityFactoryFactoryType = elementType.Assembly.GetType(generalEntityFactoryFactoryTypeName);
						if(generalEntityFactoryFactoryType == null)
						{
							return;
						}
						var methodInfo = generalEntityFactoryFactoryType.GetMethod("GetFactory", BindingFlags.Static | BindingFlags.Public, null,
																					new Type[] { typeof(Type) }, null);

						if(methodInfo == null)
						{
							return;
						}
						var parameter = LinqExpression.Parameter(typeof(Type), "p");
						var methodCallExpression = LinqExpression.Call(methodInfo, parameter);
						var lambda = LinqExpression.Lambda(methodCallExpression, parameter);
						_callGetEntityFactoryDelegate = lambda.Compile();
					}
				}
			}
		}
	}
}
