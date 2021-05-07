using System;

namespace Tortuga.TestMonkey
{
	[Flags]
	public enum TestTypes
	{
		/// <summary>
		/// Do not generate any tests for this class.
		/// </summary>
		None = 0,

		/// <summary>
		/// Read a property and assign it to itself, verifying that it hasn't changed.
		/// </summary>
		PropertySelfAssign = 1,

		/// <summary>
		/// Read the same property twice, expecting the same result both times.
		/// </summary>
		PropertyDoubleRead = 2,

		All = -1
	}


}
