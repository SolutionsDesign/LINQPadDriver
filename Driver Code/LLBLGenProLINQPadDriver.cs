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
using System.Windows.Forms;
using System.IO;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using LINQPad;
using System.Diagnostics;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Main driver class of the LLBLGen Pro LINQPad driver
	/// </summary>
	public class LLBLGenProLINQPadDriver : StaticDataContextDriver
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LLBLGenProLINQPadDriver"/> class.
		/// </summary>
		public LLBLGenProLINQPadDriver() : base() { }

		/// <summary>
		/// Returns a hierarchy of objects describing how to populate the Schema Explorer
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="customType">Type of the custom.</param>
		/// <returns></returns>
		public override List<ExplorerItem> GetSchema(IConnectionInfo cxInfo, Type customType)
		{
			return new SchemaBuilder(cxInfo, customType).GetSchema();
		}


		/// <summary>
		/// Gets the namespaces to add to the default list of namespaces added to the generateed class executed.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <returns></returns>
		public override IEnumerable<string> GetNamespacesToAdd(IConnectionInfo cxInfo)
		{
			var defaultNamespaces = new List<string>()
			{ 
				"SD.LLBLGen.Pro.LinqSupportClasses", "SD.LLBLGen.Pro.ORMSupportClasses", "SD.LLBLGen.Pro.QuerySpec", 
			};

			var templateGroup = CxInfoHelper.GetTemplateGroup(cxInfo);
			if(templateGroup == TemplateGroup.Adapter)
			{
				defaultNamespaces.Add("SD.LLBLGen.Pro.QuerySpec.Adapter");
			}
			else
			{
				defaultNamespaces.Add("SD.LLBLGen.Pro.QuerySpec.SelfServicing");
			}

			var toReturn = base.GetNamespacesToAdd(cxInfo).Union(defaultNamespaces);

			var entityAssemblyNamespaces = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.EntityAssemblyNamespacesElement);
			if(!string.IsNullOrEmpty(entityAssemblyNamespaces))
			{
				toReturn = toReturn.Union(entityAssemblyNamespaces.Split(',').Where(s=>!string.IsNullOrEmpty(s)));
			}
			return toReturn;
		}


		/// <summary>
		/// Gets the namespaces to remove from the default list of namespaces added to the generated class executed.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <returns></returns>
		public override IEnumerable<string> GetNamespacesToRemove(IConnectionInfo cxInfo)
		{
			return base.GetNamespacesToRemove(cxInfo).Union(new [] { "System.Data.Linq", "System.Data.SqlClient", "System.Data.Linq.SqlClient"});
		}


		/// <summary>
		/// Gets the custom display member provider.
		/// </summary>
		/// <param name="objectToWrite">The object to write.</param>
		/// <returns></returns>
		public override ICustomMemberProvider GetCustomDisplayMemberProvider(object objectToWrite)
		{
			var objectToWriteAsEntity = objectToWrite as IEntityCore;
			if(objectToWriteAsEntity == null)
			{
				return null;
			}
			if(typeof(ICustomMemberProvider).IsAssignableFrom(objectToWriteAsEntity.GetType()))
			{
				return null;
			}
			return new EntityMemberProvider(objectToWriteAsEntity);
		}


		/// <summary>
		/// Displays the object in grid.
		/// </summary>
		/// <param name="objectToDisplay">The object to display.</param>
		/// <param name="options">The options.</param>
		public override void DisplayObjectInGrid(object objectToDisplay, GridOptions options)
		{
			if(objectToDisplay != null)
			{
				Type elementType = LinqUtils.DetermineSetElementType(objectToDisplay.GetType());
				if(typeof(IEntityCore).IsAssignableFrom(elementType))
				{
					var membersToExclude = typeof(EntityBase).GetProperties().Select(p => p.Name)
														.Union(typeof(EntityBase2).GetProperties().Select(p => p.Name)).Distinct();
					if(typeof(IEntity).IsAssignableFrom(elementType))
					{
						// remove alwaysFetch/AlreadyFetched flag properties
						membersToExclude = membersToExclude
														.Union(elementType.GetProperties()
																		.Where(p => p.PropertyType == typeof(bool) &&
																					(p.Name.StartsWith("AlreadyFetched") || p.Name.StartsWith("AlwaysFetch")))
																		.Select(p=>p.Name));
					}
					options.MembersToExclude = membersToExclude.Distinct().ToArray();
				}
			}
			base.DisplayObjectInGrid(objectToDisplay, options);
		}


		/// <summary>
		/// Initializes the context.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="context">The context.</param>
		/// <param name="executionManager">The execution manager.</param>
		public override void InitializeContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
		{
			if(CxInfoHelper.GetEnableORMProfiler(cxInfo))
			{
				EnableORMProfiler(cxInfo);
			}

			var templateGroup = CxInfoHelper.GetTemplateGroup(cxInfo);
			switch(templateGroup)
			{
				case TemplateGroup.None:
					throw new InvalidOperationException("Template group hasn't been specified.");
				case TemplateGroup.Adapter:
					InitializeContextAdapter(cxInfo, context, executionManager);
					break;
				case TemplateGroup.SelfServicing:
					InitializeContextSelfServicing(cxInfo, context, executionManager);
					break;
				default:
					base.InitializeContext(cxInfo, context, executionManager);
					break;
			}
			var tracer = new ORMPersistenceExecutionListener(executionManager);
			Trace.Listeners.Clear();
			Trace.Listeners.Add(tracer);
			TraceHelper.PersistenceExecutionSwitch.Level = TraceLevel.Verbose;
		}


		/// <summary>
		/// Tears down the context.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="context">The context.</param>
		/// <param name="executionManager">The execution manager.</param>
		/// <param name="constructorArguments">The constructor arguments.</param>
		public override void TearDownContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager, object[] constructorArguments)
		{
			base.TearDownContext(cxInfo, context, executionManager, constructorArguments);
			ILinqMetaData contextAsLinqMetaData = context as ILinqMetaData;
			if(context != null)
			{
				var adapterToUseProperty = contextAsLinqMetaData.GetType().GetProperty("AdapterToUse");
				if(adapterToUseProperty != null)
				{
					IDataAccessAdapter adapterUsed = adapterToUseProperty.GetValue(contextAsLinqMetaData, null) as IDataAccessAdapter;
					if(adapterUsed != null)
					{
						adapterUsed.Dispose();
					}
				}
			}
		}


		/// <summary>
		/// Returns the text to display in the root Schema Explorer node for a given connection info.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <returns></returns>
		public override string GetConnectionDescription(IConnectionInfo cxInfo)
		{
			if(cxInfo == null)
			{
				return "<null>";
			}
			var templateGroup = CxInfoHelper.GetTemplateGroup(cxInfo);
			return string.Format("{0} - {1}", templateGroup.ToString(),
											  Path.GetFileNameWithoutExtension(CxInfoHelper.GetEntityAssemblyFilename(cxInfo, templateGroup)));
		}

		/// <summary>
		/// Displays a dialog prompting the user for connection details. The isNewConnection
		/// parameter will be true if the user is creating a new connection rather than editing an
		/// existing connection. This should return true if the user clicked OK. If it returns false,
		/// any changes to the IConnectionInfo object will be rolled back.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="isNewConnection">if set to <c>true</c> it's a new connection</param>
		/// <returns></returns>
		public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection)
		{
			bool toReturn = false;
			using(var dialog = new ConnectionDialog(cxInfo, isNewConnection))
			{
				var result = dialog.ShowDialog();
				toReturn = result == System.Windows.Forms.DialogResult.OK;
				if(toReturn)
				{
					ObtainAndSetEntityAssemblyNamespaces(cxInfo);
				}
			}
			return toReturn;
		}


		/// <summary>
		/// Obtains the and set entity assembly namespaces.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		private void ObtainAndSetEntityAssemblyNamespaces(IConnectionInfo cxInfo)
		{
			var entityAssemblyFilename = CxInfoHelper.GetEntityAssemblyFilename(cxInfo, CxInfoHelper.GetTemplateGroup(cxInfo));
			if(string.IsNullOrEmpty(entityAssemblyFilename))
			{
				return;
			}
			var assembly = DataContextDriver.LoadAssemblySafely(entityAssemblyFilename);
			var namespaces = assembly.GetTypes().Select(t => t.Namespace).Distinct().ToArray();
			CxInfoHelper.SetDriverDataElement(cxInfo, DriverDataElements.EntityAssemblyNamespacesElement, String.Join(",", namespaces));
		}


		/// <summary>
		/// Enables the ORM profiler.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		private void EnableORMProfiler(IConnectionInfo cxInfo)
		{
			var interceptorLocation = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.ORMProfilerInterceptorLocationElement);
			if(!File.Exists(interceptorLocation))
			{
				throw new InvalidOperationException(string.Format("The ORM Profiler interceptor isn't found at '{0}'", interceptorLocation));
			}
			var interceptorAssembly = DataContextDriver.LoadAssemblySafely(interceptorLocation);
			if(interceptorAssembly == null)
			{
				throw new InvalidOperationException(string.Format("Couldn't load the ORM Profiler interceptor assembly '{0}'", interceptorLocation));
			}
			Type interceptor = interceptorAssembly.GetType("SD.Tools.OrmProfiler.Interceptor.InterceptorCore");
			interceptor.InvokeMember("Initialize",
				System.Reflection.BindingFlags.Public |
				System.Reflection.BindingFlags.InvokeMethod |
				System.Reflection.BindingFlags.Static,
				null, null, new[] { GetConnectionDescription(cxInfo) }, System.Globalization.CultureInfo.CurrentUICulture);
		}


		/// <summary>
		/// Initializes the context for self servicing. The 'context' is an ILinqMetaData instance
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="context">The context.</param>
		/// <param name="executionManager">The execution manager.</param>
		private void InitializeContextSelfServicing(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
		{
			string connectionString = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.ConnectionStringElementName);
			if(string.IsNullOrEmpty(connectionString))
			{
				// not specified, nothing further.
				return;
			}
			// set actual connection string on CommonEntityBase.
			var selfServicingAssemblyFilename = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.SelfServicingAssemblyFilenameElement);
			var selfServicingAssembly = context.GetType().BaseType.Assembly;
			var commonDaoBaseType = selfServicingAssembly.GetTypes().Where(t=>t.Name.EndsWith("CommonDaoBase")).FirstOrDefault();
			if(commonDaoBaseType == null)
			{
				throw new InvalidOperationException(string.Format("Couldn't find type 'CommonEntityBase' in assembly '{0}'",selfServicingAssemblyFilename));
			}
			var actualConnectionStringField = commonDaoBaseType.GetField("ActualConnectionString");
			if(actualConnectionStringField == null)
			{
				throw new InvalidOperationException(string.Format("The type '{0}' doesn't have a static property ActualConnectionString.", commonDaoBaseType.FullName));
			}
			actualConnectionStringField.SetValue(null, connectionString);
		}


		/// <summary>
		/// Initializes the context for adapter. The 'context' is an ILinqMetaData instance. 
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="context">The context.</param>
		/// <param name="executionManager">The execution manager.</param>
		private void InitializeContextAdapter(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
		{
			ILinqMetaData contextAsLinqMetaData = context as ILinqMetaData;
			if(contextAsLinqMetaData == null)
			{
				throw new InvalidOperationException("'context' isn't an ILinqMetaData typed object");
			}
			string adapterAssemblyFilename = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.AdapterDBSpecificAssemblyFilenameElement);
			var adapterAssembly = DataContextDriver.LoadAssemblySafely(adapterAssemblyFilename);
			if(adapterAssembly == null)
			{
				throw new InvalidOperationException(string.Format("The file '{0}' isn't a valid assembly.", adapterAssemblyFilename));
			}
			var adapterType = adapterAssembly.GetTypes().Where(t => typeof(IDataAccessAdapter).IsAssignableFrom(t)).FirstOrDefault();
			if(adapterType == null)
			{
				throw new InvalidOperationException(string.Format("The assembly '{0}' doesn't contain an implementation of IDataAccessAdapter.", adapterAssemblyFilename));
			}
			IDataAccessAdapter adapterInstance = null;
			string connectionString = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.ConnectionStringElementName);
			if(string.IsNullOrEmpty(connectionString))
			{
				// use normal empty ctor
				adapterInstance = Activator.CreateInstance(adapterType) as IDataAccessAdapter;
			}
			else
			{
				// use ctor which specifies the ctor
				adapterInstance = Activator.CreateInstance(adapterType, connectionString) as IDataAccessAdapter;
			}
			if(adapterInstance==null)
			{
				throw new InvalidOperationException(string.Format("Couldn't create an instance of adapter type '{0}' from assembly '{1}'.", adapterType.FullName, adapterAssemblyFilename));
			}
			var adapterToUseProperty = contextAsLinqMetaData.GetType().GetProperty("AdapterToUse");
			if(adapterToUseProperty == null)
			{
				throw new InvalidOperationException(string.Format("The type '{0}' doesn't have a property 'AdapterToUse'.", context.GetType().FullName));
			}
			adapterToUseProperty.SetValue(contextAsLinqMetaData, adapterInstance, null);
		}

		#region Class Property Declarations
		/// <summary>
		/// Gets the name of the author of the driver.
		/// </summary>
		public override string Author
		{
			get { return Constants.Author; }
		}

		/// <summary>
		/// Gets the user friendly name.
		/// </summary>
		public override string Name
		{
			get { return Constants.Name; }
		}
		#endregion
	}
}
