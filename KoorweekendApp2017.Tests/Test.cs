using NUnit.Framework;
using System;
namespace KoorweekendApp2017.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase()
		{
			var yesterday = DateTime.Now.AddDays(-1);
			var now = DateTime.Now;
			var diff = yesterday - now;

			var x = diff.TotalMilliseconds;
			Assert.True(true);
		}
	}
}
