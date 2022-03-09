# Tortuga Test Monkey
Test Monkey is a source generator creates those unit tests you need, but don't want to write yourself.

## Use
Add a reference to the `Tortuga.TestMonkey` package.

```
<ItemGroup>
  <PackageReference Include="Tortuga.TestMonkey" Version="0.3.0" />
</ItemGroup>
```
For each test class, mark the class as partial add the `MakeTest` attribute. 

```
[TestClass]
[MakeTests(typeof(ItemEventArgs<string>), TestTypes.All)]
public partial class ItemEventArgsTests
{
  ...
}
```

Note: MSTest shown above. For XUnit and NUnit, you won't have a `[TestClass]` attribute.

### Choosing Tests

If you only want to include some of the possible tests, you can indicate your preference using the `TestTypes` bit flag.

```
[MakeTests(typeof(SimpleClass), TestTypes.PropertyDoubleRead | TestTypes.PropertySelfAssign)]
```

To exclude a specific test, use the `~` operator.

```
[MakeTests(typeof(AnotherClass), TestTypes.All & ~TestTypes.PropertyDoubleRead)]
```

### Supplying a Constructor

If Test Monkey cannot find a constructor for the class that it can use, then the tests will throw a `NotImplementedException`. To correct this, add the partial method described in the error message. For example, 

```
partial void CreateObject(ref Sample.UnderTest.NoDefaultConstructor? objectUnderTest)
{
	objectUnderTest = new NoDefaultConstructor("AAA", "BBB");
}
```

You may also implement the `CreateObject` method if you don't like the default behavior.

### Inspecting the Generated Tests

If you wish to see what the tests look like, add this to your project file:

```
<PropertyGroup>
  <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
  <CompilerGeneratedFilesOutputPath>Generated</CompilerGeneratedFilesOutputPath>
</PropertyGroup>

<ItemGroup>
  <!-- Don't include the output from a previous source generator execution into future runs; the */** trick here ensures that there's
  at least one subdirectory, which is our key that it's coming from a source generator as opposed to something that is coming from
  some other tool. -->
  <Compile Remove="$(CompilerGeneratedFilesOutputPath)/*/**/*.cs" />
</ItemGroup>
```

#### Excluding the Generated Tests from git

If you do not with to checkin the generated tests, add this to your `.gitignore` file.

```
**/Generated/Tortuga.TestMonkey/*
```

## Tests

### Property Self Assign

*Applies to: Public properties with a getter and setter*

This test detects problems in properties where the value changes if you assign the property to itself.

1. Reads the property
2. Assigns property to itself
3. Reads the property again and compares the result


### Property Double Read

*Applies to: Public properties with a getter*

This test detects problems in properties where the value changes if you read it multiple times. 

1. Reads the property
2. Reads the property again and compares the result

If the value is supposed to change each time the property is read, replace it with a `GetXxx` method.

## History

Test Monkey was originally created in 2011. A project dealing with electronic medical records was in dire need of unit tests, but with hundreds of classes and thousands of properties, hand-writing them all would be too time consuming. Yet with an average of one property in a hundred, not improving code coverage wasn't acceptable either. 

Not looking forward to the overtime, I created the code generator called Test Monkey. It used reflection to create the tedious property and interface unit tests, freeing up the team to concentrate on the more difficult tests.

The original Test Monkey was published on GotDotNet, which was Microsoft’s attempt at an open-source platform. To the best of my knowledge, no one else used it because the setup involved copying the projects into your solution and hard-coding paths to the output DLLs. So the code slowly rotted over time.

With C#’s Source Generators, I can finally offer the developer experience that I always wanted for Test Monkey (and learn far more than I wanted to know about Roslyn syntax trees).

### Future Tests

These tests were in the original Test Monkey. Most of them will probably be ported into Tortuga Test Monkey as I find the time.

* IBindingList
  * AddNew
  * RemoveAt
  * Remove
* ICloneable
* IComparable
  * Compare
  * Compare with Null
* IComparable\<T\>
* Constructor
* IDisposable
  * DisposeTwice
* IEditableObject
  * BeginCancel_Simple
  * BeginEnd_Simple
  * BeginCancel
  * BeginEnd
  * BeginBeginCancelCancel
  * BeginBeginCancelEnd
  * BeginBeginEndEnd
* Exception
  * Serializer <-- This is dead in .NET Core.
  * DataContractSerializer
* IList\<T\>
  * Add
  * AddClear
  * Clear
  * AddRemove
  * AddRemoveAt
  * InsertFirst
  * InsertMiddle
  * InsertLast
  * CopyTo_Middle
  * CopyTo_Front
  * CopyTo_End
  * CopyTo_Full
  * CopyTo_TooSmall
  * CopyTo_PastEnd
  * CopyTo_OOR
  * CopyTo_Null
  * AddClear_Memory
  * AddRemove_Memory
  * AddRemoveAt_Memory
* Memory
* INotifyPropertyChanged
  * NotifyPropertyChanged_SelfAssign
  * NotifyPropertyChanged
* Property
  * PropertyNullAssigned
  * PropertyAssigned


## Research Notes

* [The .NET Compiler Platform SDK](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/)
* [Source Generators Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md)

