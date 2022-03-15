namespace Sample.UnderTest;

public class SimpleClass
{
	public string? FirstName { get; set; }
	public string LastName { get; set; } = "No one";
	public string PrivateSetter { get; private set; } = "You can't touch this";
	public string? PrivateGetter { private get; set; }
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



}
