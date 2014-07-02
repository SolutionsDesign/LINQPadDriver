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
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.ComponentModel;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using LINQPad;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Simple bucket class which holds the custom state of a ResourceType
	/// </summary>
	public class EntityTypeSchemaData
	{
		/// <summary>
		/// Gets or sets the related linq meta data property.
		/// </summary>
		internal PropertyInfo LinqMetaDataProperty { get; set; }
		/// <summary>
		/// Gets or sets the type of the entity.
		/// </summary>
		internal Type EntityType { get; set; }
		/// <summary>
		/// Gets or sets the factory.
		/// </summary>
		internal IEntityFactoryCore Factory { get; set; }
		/// <summary>
		/// Gets or sets the inheritance info.
		/// </summary>
		internal IInheritanceInfo InheritanceInfo { get; set; }
		/// <summary>
		/// Gets or sets the related explorer item.
		/// </summary>
		internal ExplorerItem RelatedExplorerItem { get; set; }
		/// <summary>
		/// Gets the name of the super type.
		/// </summary>
		internal string SuperTypeName
		{
			get
			{
				if(this.InheritanceInfo == null)
				{
					return string.Empty;
				}
				return this.InheritanceInfo.SuperTypeEntityName;
			}
		}
	}
}
