using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
	class Startup
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var t = new LinqPadDriverTests();
			t.TestDialog();
		}
	}
}
