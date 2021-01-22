namespace PostTripletex.Model
{
	public class Employee
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public Department department { get; set; } = new Department();
		public Employment[] employments { get; set; } = { new Employment() };

		public string userType { get; set; } = "NO_ACCESS";
		public string dateOfBirth { get; set; }
		public string email { get; set; }
		public string phoneNumberMobile { get; set; }
	}

	public class Department
	{
		public string name { get; set; }
		// public Employee departmentManager { get; set; }
	}

	public class Employment
	{
		public string startDate { get; set; }

		public Division division { get; set; } = new Division();

		public EmploymentDetails[] employmentDetails { get; set; } = { new EmploymentDetails() };
	}

	public class Division
	{
		public string name { get; set; }
	}
}
