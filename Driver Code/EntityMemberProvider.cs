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
using LINQPad;
using System.ComponentModel;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Class which is used to provide member information to Linqpad on an entity type. 
	/// </summary>
	public class EntityMemberProvider : ICustomMemberProvider
	{
		#region Class Member Declarations
		private IEntityCore _wrappedEntity;
		private List<PropertyDescriptor> _propertyDescriptors;
		#endregion


		/// <summary>
		/// Initializes a new instance of the <see cref="EntityMemberProvider"/> class.
		/// </summary>
		/// <param name="wrappedEntity">The wrapped entity.</param>
		public EntityMemberProvider(IEntityCore wrappedEntity)
		{
			_wrappedEntity = wrappedEntity;
			_propertyDescriptors = PropertyDescriptorCache.GetDescriptors(wrappedEntity.GetType());
		}


		/// <summary>
		/// Gets the names of the available properties.
		/// </summary>
		/// <returns></returns>
		IEnumerable<string> ICustomMemberProvider.GetNames()
		{
			return _propertyDescriptors.Select(p => p.Name);
		}


		/// <summary>
		/// Gets the types of the available properties.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Type> ICustomMemberProvider.GetTypes()
		{
			return _propertyDescriptors.Select(p => p.PropertyType);
		}


		/// <summary>
		/// Gets the values of the available properties.
		/// </summary>
		/// <returns></returns>
		IEnumerable<object> ICustomMemberProvider.GetValues()
		{
			return _propertyDescriptors.Select(p => p.GetValue(_wrappedEntity));
		}
	}
}
