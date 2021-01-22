using System;
using System.Linq;
using Newtonsoft.Json;
using PostTripletex.Model;

namespace PostTripletex
{
	class ErrorHandler
	{
		public static void Handel(string json)
		{
			var error = JsonConvert.DeserializeObject<ErrorWrap>(json);
			var enumerable = error.validationMessages.Select(e => e.field + ":" + e.message);
			var message = string.Join(" ", enumerable);

			throw new ArgumentException($"StatusCode:{error.status} {message}");
		}
	}
}
