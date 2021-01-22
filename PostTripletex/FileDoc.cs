using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PostTripletex.Model;

namespace PostTripletex
{
	public class FileDoc
	{
		private const string _directory = "Data";

		public static void WriteFile(KeyInfo info, string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);
			
			if (!File.Exists(filePath))
			{
				Directory.CreateDirectory(_directory);

				using var swr = File.CreateText(filePath);
				swr.WriteLine(fileName == "Product.csv" ? "Number,Name,Id" : "Name,Id");
			}

			using var sw = File.AppendText(filePath);
			sw.WriteLine(fileName == "Product.csv" ? $"{info.number},{info.name},{info.id}" : $"{info.name},{info.id}");
		}

		public static void WriteFile(string[] info, string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			if (!File.Exists(filePath))
			{
				Directory.CreateDirectory(_directory);

				using var swr = File.CreateText(filePath);
				swr.WriteLine(fileName == "Product.csv" ? "Number,Name,Id" : "Name,Id");
			}

			foreach (var stg in info)
			{
				var strings = stg.Split(',');

				using var sw = File.AppendText(filePath);
				sw.WriteLine(fileName == "Product.csv" ? $"{strings[0]},{strings[1]},{strings[2]}" : $"{strings[0]},{strings[1]}");
			}
		}

		public static string[] ReadFile(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			return File.Exists(filePath) ? File.ReadAllLines(filePath).Skip(1).ToArray() : null;
		}

		public static void DeleteFile(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			if (File.Exists(filePath)) File.Delete(filePath);
		}

		public static string GetNumber(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			if (!File.Exists(filePath)) return "0000";

			var lastLine = File.ReadLines(filePath).Last().Split(',')[0];
			var i = (int.Parse(lastLine) + 1).ToString().PadLeft(4, '0');

			return i;
		}

		public static async Task<string[]> GetTokens()
		{
			var filePath = Path.Combine(_directory, "Tokens.txt");

			if (File.Exists(filePath)) return await File.ReadAllLinesAsync(filePath);

			Directory.CreateDirectory(_directory);

			var tokens = new string[2];

			Console.WriteLine("consumerToken");
			Console.Write("> ");
			tokens[0] = Console.ReadLine()?.Trim();

			Console.WriteLine();

			Console.WriteLine("employeeToken");
			Console.Write("> ");
			tokens[1] = Console.ReadLine()?.Trim();

			Console.WriteLine();

			return tokens;
		}
	}
}
