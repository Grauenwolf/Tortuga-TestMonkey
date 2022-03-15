namespace Sample.UnderTest;

public class NoDefaultConstructor
{
	public NoDefaultConstructor(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}

	public string? FirstName { get; set; }
	public string LastName { get; set; }


}
