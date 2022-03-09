using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tortuga.TestMonkey
{
	/// <summary>
	/// This is used to process the syntax tree. The output is "work items", which are fed into the code generators.
	/// </summary>
	/// <remarks>
	/// Created on demand before each generation pass
	/// </remarks>
	class SyntaxReceiver : ISyntaxContextReceiver
	{
		public List<string> Log { get; } = new();
		public List<WorkItem> WorkItems { get; } = new();

		/// <summary>
		/// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
		/// </summary>
		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			try
			{
				// any field with at least one attribute is a candidate for property generation
				if (context.Node is ClassDeclarationSyntax classDeclarationSyntax)
				{
					var testClass = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;
					Log.Add($"Found a class named {testClass.Name}");
					var attributes = testClass.GetAttributes();
					Log.Add($"    Found {attributes.Length} attributes");
					foreach (AttributeData att in attributes)
					{
						Log.Add($"   Attribute: {att.AttributeClass!.Name} Full Name: {att.AttributeClass.FullNamespace()}");
						foreach (var arg in att.ConstructorArguments)
						{
							Log.Add($"    ....Argument: Type='{arg.Type}' Value_Type='{arg.Value?.GetType().FullName}' Value='{arg.Value}'");

							if (arg.Value is INamedTypeSymbol namedArgType)
							{
								Log.Add($"    ........Found a INamedTypeSymbol named '{namedArgType}'");
								var members = namedArgType.GetMembers();
								foreach (var member in members)
								{
									if (member is IPropertySymbol property)
										Log.Add($"    ...........Property: {property.Name} CanRead:{property.GetMethod != null} CanWrite:{property.SetMethod != null}");
								}
							}
						}
					}

					var makeTestAttribte = testClass.GetAttributes().FirstOrDefault(att => att.AttributeClass.FullName() == "Tortuga.TestMonkey.MakeTestsAttribute");
					if (makeTestAttribte != null)
					{

						var testFramework = TestFramework.Unknown;
						foreach (var assembly in testClass.ContainingModule.ReferencedAssemblies)
						{
							if (assembly.Name == "Microsoft.VisualStudio.TestPlatform.TestFramework")
								testFramework = TestFramework.MSTest;
							else if (assembly.Name == "nunit.framework")
								testFramework = TestFramework.NUnit;
							else if (assembly.Name == "xunit.core")
								testFramework = TestFramework.XUnit;
						}


						var classUnderTest = (INamedTypeSymbol?)makeTestAttribte.ConstructorArguments[0].Value;
						var desiredTests = (TestTypes)(int)(makeTestAttribte.ConstructorArguments[1].Value ?? 0);
						if (classUnderTest != null && desiredTests != TestTypes.None && desiredTests != 0 && testFramework != TestFramework.Unknown)
						{
							WorkItems.Add(new(testClass, classUnderTest, desiredTests, testFramework));
							Log.Add($"Added work item for {classUnderTest.FullName()}!");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Add("Error parsing syntax: " + ex.ToString());
			}
		}


	}
}
