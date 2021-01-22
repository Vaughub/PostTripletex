using System;
using System.Threading.Tasks;
using RestSharp;

namespace PostTripletex
{
	public static class Put
	{
		public static async Task Subscription(string url)
		{
			if (!url.Contains("http://"))
			{
				if (!url.Contains("www.")) url = "http://www." + url;
				else url = "http://" + url;
			}

			var subId = await Get.Subscription();

			var client = new RestClient("https://api.tripletex.io/v2/");

			foreach (var id in subId)
			{
				var request = new RestRequest($"/event/subscription/{id}", Method.PUT);
				request.AddHeader("Authorization", $"Basic {Authentication.EncodedCredentials}");
				request.AddQueryParameter("fields", "id");
				request.AddJsonBody(new {targetUrl = url});

				var response = await client.ExecuteAsync(request);

				if (!response.IsSuccessful) ErrorHandler.Handel(response.Content);
			}

			Console.WriteLine("Done\n");
			Console.Write("> ");
		}
	}
}
