namespace Sample.UnderTest
{
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



	}
}
