namespace Tortuga.TestMonkey;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class MakeTestsAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="MakeTestsAttribute"></see> class.</summary>
    public MakeTestsAttribute(Type typeUnderTest, TestTypes testTypes)
    {
        TypeUnderTest = typeUnderTest;
        TestTypes = testTypes;
    }

    public TestTypes TestTypes { get; }
    public Type TypeUnderTest { get; }
}
