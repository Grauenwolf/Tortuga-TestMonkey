using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.MSTest
{
	[TestClass]
	[MakeTests(typeof(AnotherClass), TestTypes.All)]
	public partial class Test_AnotherClass
	{
	}
}
