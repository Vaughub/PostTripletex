using System.Collections.Generic;
using Newtonsoft.Json;

namespace PostTripletex.Model
{
	public class EmploymentDetails
	{
		public int percentageOfFullTimeEquivalent { get; set; }
	}

	public class Deliveryaddress
	{
		public Employee employee { get; set; }
	}

	public class CustomerCategory
	{
		public string name { get; set; }
	}
	
	public class SingleResponse<T>
	{
		[JsonProperty("value")]
		public T Value { get; set; }
	}

	public class ListResponse<T>
	{
		[JsonProperty("fullResultSize")]
		public int FullResultSize { get; set; }

		[JsonProperty("from")]
		public int From { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("versionDigest")]
		public string VersionDigest { get; set; }

		[JsonProperty("values")]
		public List<T> Values { get; set; }
	}

	public class AuthResponse
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("expirationDate")]
		public string ExpirationDate { get; set; }
	}

	public class ResponseProduct
	{
		public int id { get; set; }
		public string name { get; set; }
		public string number { get; set; }
	}

	class ResponseCustomer
	{
		public int id { get; set; }
		public string name { get; set; }
	}

	public class KeyInfo
	{
		public string name { get; set; }
		public string number { get; set; }
		public long id { get; set; }
	}

	public class ErrorWrap
	{
		public string status { get; set; }
		public ValidationMessages[] validationMessages { get; set; }

	}

	public class ValidationMessages
	{
		public string field { get; set; }
		public string message { get; set; }
		public string path { get; set; }
	}
}