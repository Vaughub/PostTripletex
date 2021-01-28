using System;
using System.Linq;
using System.Threading.Tasks;

namespace PostTripletex
{
	class Program
	{
		static async Task Main()
		{
			await Authentication.Authenticate();
			await Get.Sync();

			Console.Clear();
			Command.Welcome();

			while (true)
			{
				try
				{
					await Run();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message + "\n");
					Console.Write(">");
				}
			}
		}

		private static async Task Run()
		{
			var command = Console.ReadLine()?.Split(' ').Select(s => s.ToLower()).ToArray();

			if (command?[0] == "q") Environment.Exit(0);

			if (command?[0] == "sync")
			{
				await Get.Sync();
				Console.WriteLine("Done\n");
				Console.Write("> ");
				return;
			}

			if (command?[0] == "token")
			{
				FileDoc.DeleteFile("Tokens.txt");
				Console.Clear();

				await Authentication.Authenticate();
				await Get.Sync();
				Console.Clear();
				Command.Welcome();
				return;
			}

			if (command?[0] == "webhook" && command.Length > 1)
			{
				await Put.Subscription(command[1]);
				return;
			}

			if (command?.Length != 3 || !int.TryParse(command[1], out var number))
			{
				Command.Invalid();
				return;
			}

			if (command[0] == "post")
			{
				if (command[2] == "p") await Post.Product(number);
				else if (command[2] == "co") await Post.Contact(number);
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
}