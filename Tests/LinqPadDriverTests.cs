using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SD.LLBLGen.Pro.LINQPadDriver;

namespace Tests
{
	[TestFixture()]
	public class LinqPadDriverTests
	{
		[Test]
		[STAThread]
		public void TestDialog()
		{
			using(var dialog = new ConnectionDialog(null, true))
			{
				dialog.ShowDialog();
			}
		}
	}
}
