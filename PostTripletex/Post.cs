using System;
using System.Net;
using System.Threading.Tasks;
using PostTripletex.Model;
using RandomNameGeneratorLibrary;
using RestSharp;

namespace PostTripletex
{
	static class Post
	{
		private static readonly Random Random = new Random();

		public static async Task Contact(Employee employee, int number)
		{
			var contact = new Contact
			{
				customer =
				{
					name = "Customer",
					accountManager = employee,
					deliveryAddress = {employee = employee},
					category1 = {name = "C1"},
					category2 = {name = "C2"},
					category3 = {name = "C3"}
				}
			};

			var client = new RestClient("https://api.tripletex.io/v2/");

			var personGenerator = new PersonNameGenerator();

			for (var i = 0; i < number; i++)
			{
				var name = personGenerator.GenerateRandomFirstAndLastName().Split(' ');

				contact.firstName = name[0];
				contact.lastName = name[1];
				contact.email = name[0] + name[1] + "@gmail.com";
				contact.phoneNumberMobile = Random.Next(10000000, 99999999).ToString();
				contact.phoneNumberWork = Random.Next(10000000, 99999999).ToString();

				var request = new RestRequest("contact");

				request.AddJsonBody(contact);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				var response = await client.ExecutePostAsync<SingleValueResponse<KeyInfo>>(request);

				if (HttpStatusCode.Created != response.StatusCode) throw new ArgumentException($"Contact post error: {response.StatusCode}");

				Console.Write($"\r{i + 1} Contact made");
			}

			Console.WriteLine("\n");
			Console.Write("> ");
		}

		public static async Task Product(Employee employee, int number)
		{
			var product = new Product
			{
				supplier =
				{
					name = "Supp",
					accountManager = employee,
					deliveryAddress = {employee = employee},
					category1 = {name = "C1"},
					category2 = {name = "C2"},
					category3 = {name = "C3"}
				}
			};

			var client = new RestClient("https://api.tripletex.io/v2/");

			var placeGenerator = new PlaceNameGenerator();

			for (var i = 0; i < number; i++)
			{
				product.name = placeGenerator.GenerateRandomPlaceName();
				product.number = FileDoc.GetNumber("Product.csv");
				product.costExcludingVatCurrency = Random.Next(5, 800);
				product.priceExcludingVatCurrency = Random.Next(800, 1501);

				var request = new RestRequest("product");

				request.AddJsonBody(product);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				var response = await client.ExecutePostAsync<SingleValueResponse<KeyInfo>>(request);

				if (HttpStatusCode.Created != response.StatusCode) throw new ArgumentException($"Product post error: {response.StatusCode}");

				FileDoc.WriteFile(response.Data.Value, "Product.csv");

				Console.Write($"\r{i + 1} Product made");
			}

			Console.WriteLine("\n");
			Console.Write("> ");
		}

		public static async Task Employee(int number)
		{
			var employee = new Employee();

			var personGenerator = new PersonNameGenerator();
			var placeNameGenerator = new PlaceNameGenerator();

			var client = new RestClient("https://api.tripletex.io/v2/");

			for (var i = 0; i < number; i++)
			{
				var name = personGenerator.GenerateRandomFirstAndLastName().Split(' ');

				employee.firstName = name[0];
				employee.lastName = name[1];
				employee.email = name[0] + name[1] + "@gmail.com";
				employee.department.name = placeNameGenerator.GenerateRandomPlaceName();
				employee.phoneNumberMobile = Random.Next(10000000, 99999999).ToString();
				employee.dateOfBirth = new DateTime(Random.Next(1950, 2001), Random.Next(1, 13), Random.Next(1, 29)).ToString("yyyy-MM-dd");
				employee.employments[0].startDate = new DateTime(Random.Next(1900, 2020), Random.Next(1, 13), Random.Next(1, 29)).ToString("yyyy-MM-dd");
				employee.employments[0].division.name = placeNameGenerator.GenerateRandomPlaceName();
				employee.employments[0].employmentDetails[0].percentageOfFullTimeEquivalent = Random.Next(5, 51);

				var request = new RestRequest("employee");

				request.AddJsonBody(employee);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				var response = await client.ExecutePostAsync<SingleValueResponse<KeyInfo>>(request);

				if (HttpStatusCode.Created != response.StatusCode) throw new ArgumentException($"Employee post error: {response.StatusCode}");

				Console.Write($"\r{i + 1} Employee made");
			}

			Console.WriteLine("\n");
			Console.Write("> ");
		}

		public static async Task Customer(int number)
		{
			var customer = new Model.Customer();

			var personNameGenerator = new PersonNameGenerator();

			var client = new RestClient("https://api.tripletex.io/v2/");

			for (var i = 0; i < number; i++)
			{
				customer.name = personNameGenerator.GenerateRandomFirstAndLastName();

				var request = new RestRequest("customer");

				request.AddJsonBody(customer);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");

				var response = await client.ExecutePostAsync<SingleValueResponse<KeyInfo>>(request);

				if (HttpStatusCode.Created != response.StatusCode) throw new ArgumentException($"Customer post error: {response.StatusCode}");

				FileDoc.WriteFile(response.Data.Value, "Customer.csv");

				Console.Write($"\r{i + 1} Customer made");
			}

			Console.WriteLine("\n");
			Console.Write("> ");
		}
	}
}
