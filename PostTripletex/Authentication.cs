using System;
using System.Threading.Tasks;
using RestSharp;

namespace PostTripletex
{
	static class Authentication
	{
		private static readonly string _apiAuthEndpoint = "https://api.tripletex.io/v2/token/session/:create?";
		public static string EncodedCredentials;

		public static async Task CreateSessionToken(Credentials credentials)
		{
			var sessionToken = await GetSessionToken(credentials);
			EncodedCredentials = await ToBase64(sessionToken);
		}

		private static async Task<string> ToBase64(string sessionToken)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"0:{sessionToken}");

			return await Task.FromResult(Convert.ToBase64String(plainTextBytes));
		}

		private static async Task<string> GetSessionToken(Credentials credentials)
		{
			var client = new RestClient(_apiAuthEndpoint);
			var request = new RestRequest();

			request.AddHeader("Accept", "application/json");
			request.AddJsonBody("text/plain", "");
			request.AddQueryParameter("consumerToken", credentials.ConsumerToken);
			request.AddQueryParameter("employeeToken", credentials.EmployeeToken);
			request.AddQueryParameter("expirationDate", credentials.ExpirationDate);

			var response = await client.PutAsync<SingleValueResponse<AuthResponse>>(request);

			return response.Value.Token;
		}
	}

	public class Credentials
	{
		public string ConsumerToken { get; set; }
		public string EmployeeToken { get; set; }
		public string ExpirationDate { get; set; } = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

		public Credentials(string consumerToken, string employeeToken)
		{
			ConsumerToken = consumerToken;
			EmployeeToken = employeeToken;
		}
	}
}
