﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostTripletex
{
	public class FileDoc
	{
		public static void WriteFile(KeyInfo info, string fileName)
		{
			var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage", "Tripletex");
			var filePath = Path.Combine(directory, fileName);

			if (!File.Exists(filePath))
			{
				Directory.CreateDirectory(directory);

				using var swr = File.CreateText(filePath);
				swr.WriteLine(fileName == "Product.csv" ? "Number,Name,Id" : "Name,Id");
			}

			using var sw = File.AppendText(filePath);
			sw.WriteLine(fileName == "Product.csv" ? $"{info.number},{info.name},{info.id}" : $"{info.name},{info.id}");
		}

		public static void WriteFile(string[] info, string fileName)
		{
			var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage", "Tripletex");
			var filePath = Path.Combine(directory, fileName);

			if (!File.Exists(filePath))
			{
				Directory.CreateDirectory(directory);

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
			var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage", "Tripletex");
			var filePath = Path.Combine(directory, fileName);

			return File.Exists(filePath) ? File.ReadAllLines(filePath).Skip(1).ToArray() : null;
		}

		public static void DeleteFile(string fileName)
		{
			var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage", "Tripletex");
			var filePath = Path.Combine(directory, fileName);

			if (File.Exists(filePath)) File.Delete(filePath);
		}

		public static string GetNumber(string fileName)
		{
			var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage", "Tripletex");
			var filePath = Path.Combine(directory, fileName);

			if (!File.Exists(filePath)) return "0000";

			var lastLine = File.ReadLines(filePath).Last().Split(',')[0];
			var i = (int.Parse(lastLine) + 1).ToString().PadLeft(4, '0');

			return i;
		}

		public static async Task GetTokens()
		{
			if (File.Exists("Tokens.txt"))
			{
				var tokens = await File.ReadAllLinesAsync("Tokens.txt");

				await Authentication.CreateSessionToken(new Credentials(tokens[0], tokens[1]));
			}
			else
			{
				var tokens = new string[2];

				Console.WriteLine("consumerToken");
				Console.Write("> ");
				tokens[0] = Console.ReadLine()?.Trim();

				Console.WriteLine();

				Console.WriteLine("employeeToken");
				Console.Write("> ");
				tokens[1] = Console.ReadLine()?.Trim();

				Console.WriteLine();

				await Authentication.CreateSessionToken(new Credentials(tokens[0], tokens[1]));

				File.WriteAllLines("Tokens.txt", tokens);
			}
		}
	}
}
