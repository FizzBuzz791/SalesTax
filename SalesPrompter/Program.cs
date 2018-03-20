using System;
using System.IO;
using SalesTax;
using SalesTax.Models;
using SalesTax.Printers;

namespace SalesPrompter
{
	internal class Program
	{
		private static void Main()
		{
			Sale sale = new Sale();
			Console.WriteLine("Enter sales in the format <qty> <description> at <unit price>\nFor example: 2 books at 13.25\nEntering a blank line completes the sale\n");
			string input = GetInput();
			while (!string.IsNullOrEmpty(input))
			{
				if (!sale.Add(input))
					Console.WriteLine("Sales should be in the format of <qty> <description> at <unit price>\nFor example: 2 books at 13.25");
				input = GetInput();
			}

			IPrinter receiptPrinter = new BasicPrinter();
			Console.WriteLine(receiptPrinter.PrintReceipt(sale));
			Console.WriteLine("--- Press Enter to Finish ---");
			Console.ReadLine();
		}

		private static string GetInput()
		{
			string result;
			Console.Write("Sale : ");
			try
			{
				result = Console.ReadLine();
			}
			catch (IOException)
			{
				result = string.Empty;
			}
			if (!string.IsNullOrEmpty(result))
				result = result.Trim();
			return result;
		}
	}
}