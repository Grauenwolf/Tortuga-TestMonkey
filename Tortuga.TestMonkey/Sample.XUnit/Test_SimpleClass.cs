using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.XUnit;

[MakeTests(typeof(SimpleClass), TestTypes.All)]
public partial class Test_SimpleClass
{
}
