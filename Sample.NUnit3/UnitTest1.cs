using NUnit.Framework;
using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.NUnit3
{
	[MakeTests(typeof(SimpleClass), TestTypes.All)]
	public partial class Test_SimpleClass
	{
	}

	[MakeTests(typeof(AnotherClass), TestTypes.All)]
	public partial class Test_AnotherClass
	{
	}

	[MakeTests(typeof(NoDefaultConstructor), TestTypes.All)]
	public partial class Test_NoDefaultConstructor
	{
		partial void CreateObject(ref Sample.UnderTest.NoDefaultConstructor? objectUnderTest)
		{
			objectUnderTest = new NoDefaultConstructor("AAA", "BBB");
		}
	}

	[MakeTests(typeof(SimplePair<int, string>), TestTypes.All)]
	public partial class Test_SimplePair
	{
		partial void CreateObject(ref Sample.UnderTest.SimplePair<System.Int32, System.String>? objectUnderTest)
		{
			objectUnderTest = new SimplePair<int, string>(123, "BBB");
		}
	}
}