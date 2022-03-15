using System.Text;

namespace Tortuga.TestMonkey;

class CodeWriter
{
	public CodeWriter(TestFramework testFramework)
	{
		TestFramework = testFramework;
		m_ScopeTracker = new(this); //We only need one. It can be reused.
	}
	public TestFramework TestFramework { get; }
	StringBuilder Content { get; } = new();
	int IndentLevel { get; set; }
	ScopeTracker m_ScopeTracker { get; } //We only need one. It can be reused.
	public void Append(string line) => Content.Append(line);

	public void AppendLine(string line) => Content.Append(new string('\t', IndentLevel)).AppendLine(line);
	public void AppendLine() => Content.AppendLine();
	public IDisposable BeginScope(string line)
	{
		AppendLine(line);
		return BeginScope();
	}
	public IDisposable BeginScope()
	{
		Content.Append(new string('\t', IndentLevel)).AppendLine("{");
		IndentLevel += 1;
		return m_ScopeTracker;
	}

	public void EndLine() => Content.AppendLine();

	public void EndScope()
	{
		IndentLevel -= 1;
		Content.Append(new string('\t', IndentLevel)).AppendLine("}");
	}

	public void StartLine() => Content.Append(new string('\t', IndentLevel));
	public override string ToString() => Content.ToString();

	public void AddTestFramework()
	{
		switch (TestFramework)
		{
			case TestFramework.MSTest:
				AppendLine("using Microsoft.VisualStudio.TestTools.UnitTesting;");
				break;
			case TestFramework.XUnit:
				AppendLine("using Xunit;");
				break;
			case TestFramework.NUnit:
				AppendLine("using NUnit.Framework;");
				break;
		}
	}

	public IDisposable StartTest(string testName)
	{
		switch (TestFramework)
		{
			case TestFramework.MSTest:
				AppendLine("[TestMethod]");
				break;
			case TestFramework.XUnit:
				AppendLine("[Fact]");
				break;
			case TestFramework.NUnit:
				AppendLine("[Test]");
				break;
		}
		return BeginScope($"public void @{testName}()");
	}
	public void AssertAreEqual(string expected, string actual, string message)
	{
		switch (TestFramework)
		{
			case TestFramework.MSTest:
				AppendLine($@"Assert.AreEqual({expected}, {actual}, ""{EscapeString(message)}"");");
				break;
			case TestFramework.XUnit:
				AppendLine($@"Assert.True({expected} == {actual}, $""Expected value {{{expected}}} did not equal actual value {{{actual}}}. {EscapeString(message)}"");");
				break;
			case TestFramework.NUnit:
				AppendLine($@"Assert.AreEqual({expected}, {actual}, ""{EscapeString(message)}"");");
				break;
		}
	}

	string EscapeString(string text) => text.Replace("\"", "\"\"");

	class ScopeTracker : IDisposable
	{
		public ScopeTracker(CodeWriter parent)
		{
			Parent = parent;
		}
		public CodeWriter Parent { get; }

		public void Dispose()
		{
			Parent.EndScope();
		}
	}
}
