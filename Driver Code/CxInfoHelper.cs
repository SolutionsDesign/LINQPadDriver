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
using System.Xml.Linq;
using System.Xml;
using SD.LLBLGen.Pro.LinqSupportClasses;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Class which contains helper logic to work with the CxInfo object.
	/// </summary>
	internal class CxInfoHelper
	{
		/// <summary>
		/// Creates a new element in _cxInfo.DriverData with the name specified.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="elementName">Name of the element.</param>
		internal static void CreateDriverDataElement(IConnectionInfo cxInfo, string elementName)
		{
			if(cxInfo.DriverData.Element(elementName) == null)
			{
				cxInfo.DriverData.Add(new XElement(elementName) { Value = string.Empty });
			}
		}


		/// <summary>
		/// Gets the value of the cxDriverData Xelement with the name specified
		/// </summary>
		/// <param name="elementName">Name of the element.</param>
		/// <returns>the value of the element or string.empty if not found</returns>
		internal static string GetDriverDataElementValue(IConnectionInfo cxInfo, string elementName)
		{
			if(cxInfo == null)
			{
				return string.Empty;
			}
			var element = cxInfo.DriverData.Element(elementName);
			return element == null ? string.Empty : element.Value;
		}


		/// <summary>
		/// Sets the value of the cxinfo.DriverData Xelement with the name specified  to the value specified. 
		/// </summary>
		/// <param name="elementName">Name of the element.</param>
		/// <param name="value">The value. Has to be converted with Xmlconvert to be able to properly read it back</param>
		internal static void SetDriverDataElement(IConnectionInfo cxInfo, string elementName, string value)
		{
			if(cxInfo == null)
			{
				return;
			}
			var element = cxInfo.DriverData.Element(elementName);
			if(element != null)
			{
				element.Value = value;
			}
		}

		/// <summary>
		/// Gets whether ORM profiler interceptor should be enabled or not. 
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <returns></returns>
		internal static bool GetEnableORMProfiler(IConnectionInfo cxInfo)
		{
			string value = GetDriverDataElementValue(cxInfo, DriverDataElements.EnableORMProfilerElement);
			if(string.IsNullOrEmpty(value))
			{
				return false;
			}
			return XmlConvert.ToBoolean(value);
		}


		/// <summary>
		/// Gets the template group value from the specified cxInfo
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <returns></returns>
		internal static TemplateGroup GetTemplateGroup(IConnectionInfo cxInfo)
		{
			if(cxInfo == null)
			{
				return TemplateGroup.None;
			}
			string rawValue = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.TemplateGroupElement);
			if(string.IsNullOrEmpty(rawValue))
			{
				return TemplateGroup.None;
			}
			return (TemplateGroup)XmlConvert.ToInt32(rawValue);
		}


		/// <summary>
		/// Gets the entity assembly filename.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="templateGroup">The template group.</param>
		/// <returns></returns>
		internal static string GetEntityAssemblyFilename(IConnectionInfo cxInfo, TemplateGroup templateGroup)
		{
			string assemblyFileNameTouse = string.Empty;
			switch(templateGroup)
			{
				case TemplateGroup.None:
				case TemplateGroup.SelfServicing:
					assemblyFileNameTouse = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.SelfServicingAssemblyFilenameElement);
					break;
				case TemplateGroup.Adapter:
					assemblyFileNameTouse = CxInfoHelper.GetDriverDataElementValue(cxInfo, DriverDataElements.AdapterDBGenericAssemblyFilenameElement);
					break;
			}
			return assemblyFileNameTouse;
		}


		/// <summary>
		/// Creates the driver data elements in cxInfo.DriverData
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		internal static void CreateDriverDataElements(IConnectionInfo cxInfo)
		{
			if(cxInfo == null)
			{
				return;
			}
			CreateDriverDataElement(cxInfo, DriverDataElements.AdapterDBGenericAssemblyFilenameElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.AdapterDBSpecificAssemblyFilenameElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.ConfigFileFilenameElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.ConnectionStringElementName);
			CreateDriverDataElement(cxInfo, DriverDataElements.EnableORMProfilerElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.SelfServicingAssemblyFilenameElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.TemplateGroupElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.ORMProfilerInterceptorLocationElement);
			CreateDriverDataElement(cxInfo, DriverDataElements.EntityAssemblyNamespacesElement);
		}


		/// <summary>
		/// Gets the ILinqMetaData type from the entity assembly.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="entityAssemblyFilename">The entity assembly filename.</param>
		/// <returns>the typename of the ILinqMetaData implementing type or the empty string if not found</returns>
		/// <remarks>Sets cxInfo.CustomTypeInfo.CustomAssemblyPath</remarks>
		internal static string GetILinqMetaDataTypeFromEntityAssembly(IConnectionInfo cxInfo, string entityAssemblyFilename)
		{
			if(cxInfo == null)
			{
				return string.Empty;
			}
			if(string.IsNullOrEmpty(entityAssemblyFilename))
			{
				return string.Empty;
			}
			cxInfo.CustomTypeInfo.CustomAssemblyPath = entityAssemblyFilename;
			var linqMetaDataTypes = cxInfo.CustomTypeInfo.GetCustomTypesInAssembly(typeof(ILinqMetaData).FullName);
			return linqMetaDataTypes.FirstOrDefault() ?? string.Empty;
		}


		/// <summary>
		/// Sets the custom type info, which is the ILinqMetaData type in the specified assembly.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="templateGroup">The template group.</param>
		internal static void SetCustomTypeInfo(IConnectionInfo cxInfo, TemplateGroup templateGroup)
		{
			if(cxInfo==null)
			{
				return;
			}

			string assemblyFilename = GetEntityAssemblyFilename(cxInfo, templateGroup);
			if(string.IsNullOrEmpty(assemblyFilename))
			{
				return;
			}
			cxInfo.CustomTypeInfo.CustomTypeName = GetILinqMetaDataTypeFromEntityAssembly(cxInfo, assemblyFilename);
		}
	}
}
