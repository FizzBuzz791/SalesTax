using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTax
{
	public class SaleLine
	{
		#region Public Properties
		public string ProductName { get; }
		public decimal Price { get; }
		public bool IsImported { get; }
		public int Quantity { get; }
		public decimal LineValue { get; }
		public decimal Tax { get; }
		#endregion

		/// <summary>
		/// Public constructor for the sale line
		/// </summary>
		/// <param name="lineQuantity">Quantity on order</param>
		/// <param name="name">name of the product</param>
		/// <param name="unitPrice">price of a single item</param>
		/// <param name="itemIsImported">flag to indicate if the item is imported</param>
		public SaleLine(int lineQuantity, string name, decimal unitPrice, bool itemIsImported)
		{
			if (string.IsNullOrEmpty(name)) 
				name = string.Empty;

			Quantity = lineQuantity;
			ProductName = name;
			Price = unitPrice;
			IsImported = itemIsImported;
			LineValue = Price * Quantity;

			// calculate taxable amount
			// ideally should really have a product list and tax rules, but this'll have to do for the exercise.
			List<string> taxFreeItems = new List<string>
			{
				"book",
				"tablet",
				"chip",
				"chocolate"
			};

			// No base tax applicable for books, medicals items or food. 10% base tax for general items.
			int taxRate = taxFreeItems.Any(t => ProductName.Contains(t)) ? 0 : 10;

			if (IsImported)
				taxRate += 5; // Extra 5% regardless for any imported items

			Tax = CalculateTax(LineValue,taxRate);
			// Add tax to line value
			LineValue += Tax;
		}

		/// <summary>
		/// Calculates the amount of tax for a value, rounded up to the nearest 5 cents
		/// </summary>
		/// <param name="value">The original value</param>
		/// <param name="taxRate">The tax rate to apply</param>
		/// <returns>The calculated tax on the original value</returns>
		public static decimal CalculateTax(decimal value, int taxRate)
		{
			double amount = (double) Math.Round(value * taxRate / 100, 2);

			//Now round up to nearest 5 cents.
			double remainder = amount % .05;
			if (remainder > 0)
				amount += .05 - remainder;
			return (decimal)amount;
		}

		/// <summary>
		/// Converts the sale line to a string
		/// </summary>
		/// <returns>The string representation of the sale line</returns>
		public override string ToString()
		{
			return $"{Quantity} {ProductName}: {LineValue:#,##0.00}";
		}
	}
}