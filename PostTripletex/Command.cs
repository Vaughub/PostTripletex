﻿using System;

namespace PostTripletex
{
	public static class Command
	{
		public static void Welcome()
		{
			Console.WriteLine("Post/Delete Tripletex");
			Console.WriteLine("  Commands:");
			Console.WriteLine("     q    - quit program");
			Console.WriteLine("     sync - sync to local file");
			Console.WriteLine("     token - delete tokens");
			Console.WriteLine("     post - post to Tripletex");
			Console.WriteLine("     del  - delete from Tripletex");
			Console.WriteLine("  Options:");
			Console.WriteLine("     p(product)   - Post/Del");
			Console.WriteLine("     cu(customer) - Post/Del");
			Console.WriteLine("     co(contact)  - Post");
			Console.WriteLine("     e(employee)  - Post");
			Console.WriteLine("\nExample: del 5 p (deletes 5 products)");
			Console.Write("\n> ");
		}

		public static void Invalid()
		{
			Console.WriteLine("Invalid command");
			Console.Write("\n> ");
		}
	}
}
