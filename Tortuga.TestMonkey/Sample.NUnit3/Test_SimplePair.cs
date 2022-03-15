using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.NUnit3
{
	[MakeTests(typeof(SimplePair<int, string>), TestTypes.All)]
	public partial class Test_SimplePair
	{
		partial void CreateObject(ref SimplePair<int, string>? objectUnderTest)
		{
			objectUnderTest = new SimplePair<int, string>(123, "BBB");
		}
	}
}
