using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace PostTripletex
{
	public static class Get
	{
		public static async Task Sync()
		{
			await Product();
			await Customer();

			Console.WriteLine("Sync complete\n");
			Console.Write("> ");
		}

		public static async Task Product()
		{
			var client = new RestClient("https://api.tripletex.io/v2/");

			var request = new RestRequest("product");
			request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

			var response = await client.ExecuteGetAsync<ListResponse<ResponseProduct>>(request);

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

			var data = response.Data.Values.Select(d => $"{d.name},{d.id}").ToArray();

			FileDoc.DeleteFile("Customer.csv");

			FileDoc.WriteFile(data, "Customer.csv");
		}
	}
}