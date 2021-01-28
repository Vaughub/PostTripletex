using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostTripletex
{
	public class FileDoc
	{
		private static readonly string _directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PostTripletex";

		public static void WriteFile(string[] info, string fileName, string startLine = "")
		{
			var filePath = Path.Combine(_directory, fileName);
				
			if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);

			if (!string.IsNullOrEmpty(startLine))
			{
				File.WriteAllText(filePath, startLine + "\n");
				File.AppendAllLines(filePath, info);
			}
			else
			{
				File.WriteAllLines(filePath, info);
			}
		}

		public static void AppendFile(string info, string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);

			File.AppendAllText(filePath, info + "\n");
		}

		public static string[] ReadFile(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			return File.Exists(filePath) ? File.ReadAllLines(filePath).Skip(1).ToArray() : null;
		}

		public static void DeleteFile(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			File.Delete(filePath);
		}

		public static string GetNumber(string fileName)
		{
			var filePath = Path.Combine(_directory, fileName);

			if (!File.Exists(filePath)) return "0000";

			var lastLine = File.ReadLines(filePath).Last().Split(',')[0];

			return int.TryParse(lastLine, out var numb) ? (numb + 1).ToString().PadLeft(4, '0') : "0000";
		}

		public static async Task<string[]> GetTokens()
		{
			var filePath = Path.Combine(_directory, "Tokens.txt");

			if (File.Exists(filePath)) return await File.ReadAllLinesAsync(filePath);

			Directory.CreateDirectory(_directory);

			var tokens = new string[2];

			Console.WriteLine("ConsumerToken");
			Console.Write("> ");
			tokens[0] = Console.ReadLine()?.Trim();

			Console.WriteLine();

			Console.WriteLine("EmployeeToken");
			Console.Write("> ");
			tokens[1] = Console.ReadLine()?.Trim();

			Console.WriteLine();

			return tokens;
		}
	}
}
