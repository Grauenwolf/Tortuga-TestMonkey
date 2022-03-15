using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.MSTest
{
	[TestClass]
	[MakeTests(typeof(NoDefaultConstructor), TestTypes.All)]
	public partial class Test_NoDefaultConstructor
	{
		partial void CreateObject(ref NoDefaultConstructor? objectUnderTest)
		{
			objectUnderTest = new NoDefaultConstructor("AAA", "BBB");
		}
	}
}
