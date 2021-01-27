using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace PostTripletex
{
	public static class Delete
	{
		public static async Task Product(int number)
		{
			var readFile = FileDoc.ReadFile("Product.csv");

			if (readFile == null)
			{
				Console.WriteLine("Nothing to delete");
				return;
			}

			var productNumber = readFile.Select(f => f.Split(',')[2]).ToArray();

			var client = new RestClient("https://api.tripletex.io/v2/");

			for (var i = productNumber.Length - 1; i >= (productNumber.Length - number < 0 ? 0 : productNumber.Length - number); i--)
			{
				var request = new RestRequest("product/" + productNumber[i], Method.DELETE);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				await client.ExecuteAsync(request);

				Console.Write($"\r{productNumber.Length - i} Product deleted");
			}

			if (productNumber.Length - number > 0) FileDoc.WriteFile(readFile.Take(readFile.Length - number).ToArray(), "Product.csv", "Number,Name,Id");

			Console.WriteLine("\n");
			Console.Write("> ");
		}

		public static async Task Customer(int number)
		{
			var readFile = FileDoc.ReadFile("Customer.csv");

			if (readFile == null)
			{
				Console.WriteLine("Nothing to delete");
				return;
			}

			var productNumber = readFile.Select(f => f.Split(',')[1]).ToArray();

			var client = new RestClient("https://api.tripletex.io/v2/");

			for (var i = productNumber.Length - 1; i >= (productNumber.Length - number < 0 ? 0 : productNumber.Length - number); i--)
			{
				var request = new RestRequest("customer/" + productNumber[i], Method.DELETE);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				await client.ExecuteAsync(request);

				Console.Write($"\r{productNumber.Length - i} Customer deleted");
			}

			if (productNumber.Length - number > 0) FileDoc.WriteFile(readFile.Take(readFile.Length - number).ToArray(), "Customer.csv", "Name,Id");

			Console.WriteLine("\n");
			Console.Write("> ");
		}
	}
}
