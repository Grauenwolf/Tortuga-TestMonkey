using Microsoft.CodeAnalysis;
using System;

namespace Tortuga.TestMonkey
{
    class WorkItems
    {
        public WorkItems(INamedTypeSymbol testClass, INamedTypeSymbol classUnderTest, TestTypes testTypes, TestFramework testFramework)
        {
            TestClass = testClass ?? throw new ArgumentNullException(nameof(testClass));
            ClassUnderTest = classUnderTest ?? throw new ArgumentNullException(nameof(classUnderTest));
            TestTypes = testTypes;
            TestFramework = testFramework;
        }

        public INamedTypeSymbol ClassUnderTest { get; }
        public INamedTypeSymbol TestClass { get; }
        public TestFramework TestFramework { get; }
        public TestTypes TestTypes { get; }
    }
}
