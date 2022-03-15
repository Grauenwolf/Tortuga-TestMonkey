using Sample.UnderTest;
using Tortuga.TestMonkey;

namespace Sample.NUnit3;

[MakeTests(typeof(SimpleClass), TestTypes.All)]
public partial class Test_SimpleClass
{
}
