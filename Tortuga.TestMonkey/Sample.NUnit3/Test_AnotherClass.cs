using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.NUnit3
{
	[MakeTests(typeof(AnotherClass), TestTypes.All)]
	public partial class Test_AnotherClass
	{
	}
}
