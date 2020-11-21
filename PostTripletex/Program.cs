using System;
using System.Threading.Tasks;
using PostTripletex.Model;

namespace PostTripletex
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var consumerToken = "";
			var employeeToken = "";

			// test
			await Authentication.CreateSessionToken(new Credentials(consumerToken, employeeToken));

			Command.Welcome();

			while (true)
			{
				var command = Console.ReadLine()?.Split(' ');

				if (command?[0] == "q") break;

				if (command?[0] == "sync")
				{
					await Get.Sync();
					continue;
				}

				if (command?.Length != 3 || !int.TryParse(command[1], out var number))
				{
					Command.Invalid();
					continue;
				}

				if (command[0].ToLower() == "post")
				{
					if (command[2].ToLower() == "p") await Post.Product(MakeEmployee(), number);
					else if (command[2].ToLower() == "co") await Post.Contact(MakeEmployee(), number);
					else if (command[2].ToLower() == "e") await Post.Employee(number);
					else if (command[2].ToLower() == "cu") await Post.Customer(number);
					else Command.Invalid();
				}
				else if (command[0].ToLower() == "del")
				{
					if (command[2].ToLower() == "p") await Delete.Product(number);
					else if (command[2].ToLower() == "cu") await Delete.Customer(number);
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
	}
}