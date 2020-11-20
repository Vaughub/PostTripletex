using PostTripletex.Model;

namespace PostTripletex
{
	public class Product
	{
		public string name { get; set; }
		public string number { get; set; }
		public int costExcludingVatCurrency { get; set; }
		public int priceExcludingVatCurrency { get; set; }
		public int stockOfGoods { get; set; }
		public bool isStockItem { get; set; }
		public Supplier supplier { get; set; } = new Supplier();
	}

	public class Supplier
	{
		public string name { get; set; }
		public Employee accountManager { get; set; }
		public Deliveryaddress deliveryAddress { get; set; } = new Deliveryaddress();
		public CustomerCategory category1 { get; set; } = new CustomerCategory();
		public CustomerCategory category2 { get; set; } = new CustomerCategory();
		public CustomerCategory category3 { get; set; } = new CustomerCategory();
	}

	public class ProductUnit
	{
		public string name { get; set; }
		public string nameShort { get; set; }
		public string commonCode { get; set; }
	}

	public class KeyInfo
	{
		public string name { get; set; }
		public string number { get; set; }
		public long id { get; set; }
	}
}
