using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PostTripletex.Model;

namespace PostTripletex
{
	class Program
	{
		static async Task Main()
		{
			await Authenticate();

			Command.Welcome();

			while (true)
			{
				var command = Console.ReadLine()?.Split(' ').Select(s => s.ToLower()).ToArray();

				if (command?[0] == "q") break;

				if (command?[0] == "sync")
				{
					await Get.Sync();
					continue;
				}

				if (command?[0] == "token")
				{
					File.Delete("Tokens.txt");
					Console.WriteLine("Tokens deleted\n");

					await Authenticate();
				}

				if (command?.Length != 3 || !int.TryParse(command[1], out var number))
				{
					Command.Invalid();
					continue;
				}

				if (command[0] == "post")
				{
					if (command[2] == "p") await Post.Product(MakeEmployee(), number);
					else if (command[2] == "co") await Post.Contact(MakeEmployee(), number);
					else if (command[2] == "e") await Post.Employee(number);
					else if (command[2] == "cu") await Post.Customer(number);
					else Command.Invalid();
				}
				else if (command[0] == "del")
				{
					if (command[2] == "p") await Delete.Product(number);
					else if (command[2] == "cu") await Delete.Customer(number);
					else Command.Invalid();
				}
				else Command.Invalid();
			}
		}

		private static Employee MakeEmployee()
		{
			var employee = new Employee {firstName = "Ola", lastName = "Kam", department = {name = "Nextcom"}};
			employee.employments[0].startDate = "2020-11-05";
			employee.employments[0].division.name = "TheKids";
			employee.employments[0].employmentDetails[0].percentageOfFullTimeEquivalent = 25;

			return employee;
		}

		private static async Task Authenticate()
		{
			while (true)
			{
				try
				{
					await FileDoc.GetTokens();

					break;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message + "\n");
				}
			}
		}
	}
}