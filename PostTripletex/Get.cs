using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PostTripletex.Model;
using RestSharp;

namespace PostTripletex
{
	public static class Get
	{
		public static async Task Sync()
		{
			await Product();
			await Customer();
		}

		public static async Task Product()
		{
			var client = new RestClient("https://api.tripletex.io/v2/");

			var request = new RestRequest("product");
			request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

			var response = await client.ExecuteGetAsync<ListResponse<ResponseProduct>>(request);

			if (!response.IsSuccessful) ErrorHandler.Handel(response.Content);

			var data = response.Data.Values.Select(d => $"{d.number},{d.name},{d.id}").ToArray();

			FileDoc.DeleteFile("Product.csv");

			FileDoc.WriteFile(data, "Product.csv");
		}

		public static async Task Customer()
		{
			var client = new RestClient("https://api.tripletex.io/v2/");

			var request = new RestRequest("customer");
			request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

			var response = await client.ExecuteGetAsync<ListResponse<ResponseCustomer>>(request);

			if (!response.IsSuccessful) ErrorHandler.Handel(response.Content);

			var data = response.Data.Values.Select(d => $"{d.name},{d.id}").ToArray();

			FileDoc.DeleteFile("Customer.csv");

			FileDoc.WriteFile(data, "Customer.csv");
		}

		public static async Task<long[]> Subscription()
		{
			var client = new RestClient("https://api.tripletex.io/v2/");

			var request = new RestRequest("/event/subscription");

			request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");
			request.AddQueryParameter("fields", "id");

			var response = await client.ExecuteGetAsync<ListResponse<KeyInfo>>(request);

			if (!response.IsSuccessful) ErrorHandler.Handel(response.Content);

			return response.Data.Values.Select(s => s.id).ToArray();
		}
	}
}