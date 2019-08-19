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

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Constants used in the driver.
	/// </summary>
	internal class Constants
	{
		internal static readonly int MajorVersion = 5;
		internal static readonly int MinorVersion = 5;
		internal static readonly string Author = "Solutions Design bv";
		internal static readonly string Name = "LLBLGen Pro v" + string.Format("{0}.{1}", MajorVersion, MinorVersion) + " LINQPad driver";
	}

	/// <summary>
	/// Constants to use as element names for CxInfo.DriverData xelements.
	/// </summary>
	internal class DriverDataElements
	{
		internal static readonly string TemplateGroupElement = "TemplateGroupElement";
		internal static readonly string SelfServicingAssemblyFilenameElement = "SelfServicingAssemblyFilenameElement";
		internal static readonly string AdapterDBGenericAssemblyFilenameElement = "AdapterDBGenericAssemblyFilenameElement";
		internal static readonly string AdapterDBSpecificAssemblyFilenameElement = "AdapterDBSpecificAssemblyFilenameElement";
		internal static readonly string ConnectionStringElementName = "ConnectionStringElement";
		internal static readonly string ConfigFileFilenameElement = "ConfigFileFilenameElement";
		internal static readonly string EnableORMProfilerElement = "EnableORMProfilerElement";
		internal static readonly string ORMProfilerInterceptorLocationElement = "ORMProfilerInterceptorLocationElement";
		internal static readonly string EntityAssemblyNamespacesElement = "EntityAssemblyNamespacesElement";
	}

	/// <summary>
	/// Enum for the template group specification
	/// </summary>
	internal enum TemplateGroup
	{
		None,
		SelfServicing,
		Adapter
	}
}
