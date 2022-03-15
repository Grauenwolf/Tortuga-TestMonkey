using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.XUnit
{
	[MakeTests(typeof(AnotherClass), TestTypes.All)]
	public partial class Test_AnotherClass
	{
	}
}
