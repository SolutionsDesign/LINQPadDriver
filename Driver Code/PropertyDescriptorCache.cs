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
using System.ComponentModel;
using System.Collections;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Simple cache to re-use property descriptor lists for entity types.
	/// </summary>
	internal static class PropertyDescriptorCache
	{
		#region Class Member Declarations
		private static object _semaphore = new object();
		private static Dictionary<Type, List<PropertyDescriptor>> _cache = new Dictionary<Type, List<PropertyDescriptor>>();
		#endregion


		/// <summary>
		/// Gets the property descriptors for the type specified. If they're not available in the cache, they're added. 
		/// </summary>
		/// <param name="entityType">Type of the entity.</param>
		/// <returns></returns>
		/// <remarks>Only returns descriptors for properties which don't implement IEntityCollectionCore, 
		/// don't implement IEntityCore and which are browsable.</remarks>
		internal static List<PropertyDescriptor> GetDescriptors(Type entityType)
		{
			lock(_semaphore)
			{
				List<PropertyDescriptor> descriptors = null;
				if(!_cache.TryGetValue(entityType, out descriptors))
				{
					descriptors = new List<PropertyDescriptor>();
					var rawDescriptors = TypeDescriptor.GetProperties(entityType);
					foreach(PropertyDescriptor descriptor in rawDescriptors)
					{
						if(!descriptor.IsBrowsable || typeof(IEntityCollectionCore).IsAssignableFrom(descriptor.PropertyType) || 
							typeof(IEntityCore).IsAssignableFrom(descriptor.PropertyType))
						{
							continue;
						}
						descriptors.Add(descriptor);
					}
					_cache[entityType] = descriptors;
				}
				return descriptors;
			}
		}
	}
}
