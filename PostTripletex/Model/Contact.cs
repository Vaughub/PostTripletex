namespace PostTripletex.Model
{
	public class Contact
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string email { get; set; }
		public Customer customer { get; set; } = new Customer();

		public string phoneNumberMobile { get; set; }
		public string phoneNumberWork { get; set; }
	}

	public class Customer
	{
		public string name { get; set; }
		public Employee accountManager { get; set; }
		public Deliveryaddress deliveryAddress { get; set; } = new Deliveryaddress();
		public CustomerCategory category1 { get; set; } = new CustomerCategory();
		public CustomerCategory category2 { get; set; } = new CustomerCategory();
		public CustomerCategory category3 { get; set; } = new CustomerCategory();
	}
}
