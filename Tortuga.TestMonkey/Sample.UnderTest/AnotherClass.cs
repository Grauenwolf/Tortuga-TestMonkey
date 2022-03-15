using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.UnderTest;

public class AnotherClass
{
	public string? FirstName { get; set; }
	public string LastName { get; set; } = "No one";
	public string IHaveNoSetter { get; private set; } = "You can't touch this";
	public string? IHaveNoGetter { private get; set; }
	public string FullName => FirstName + " " + LastName;

	int m_BadProperty1;
	public int BadProperty1
	{
		get => m_BadProperty1;
		set { m_BadProperty1 = value + 1; }
	}

	int m_BadProperty2;
	public int BadProperty2
	{
		get => m_BadProperty2++;
	}

	int m_Age;

	/// <summary>
	/// Gets or sets the age.
	/// </summary>
	/// <value>The age.</value>
	/// <exception cref="ArgumentOutOfRangeException">Age - Age must be between 0 and 120</exception>
	[Range(0, 120)]
	public int Age
	{
		get => m_Age;
		set
		{
			if (value < 0 || value > 120)
				throw new ArgumentOutOfRangeException(nameof(Age), "Age must be between 0 and 120");
			m_Age = value;
		}
	}

}
