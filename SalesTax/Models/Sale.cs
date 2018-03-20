using System.Collections.Generic;

namespace SalesTax.Models
{
	public class Sale
	{
		public List<SaleLine> SaleLines { get; }

		/// <summary>
		/// The total Tax amount for the sale
		/// </summary>
		public decimal Tax { get; private set; }

		/// <summary>
		/// The total value of the sale (including Tax)
		/// </summary>
		public decimal TotalValue { get; private set; }

		public Sale()
		{
			SaleLines = new List<SaleLine>();
		}

		/// <summary>
		/// Adds a line to the sale.
		/// </summary>
		/// <param name="inputLine">The line to add.</param>
		/// <returns>True for success, False for failure.  Failures are usually caused via incorrect formatting of the input</returns>
		public bool Add(string inputLine)
		{
			bool success = false;

			SaleLine saleLine = InputParser.ProcessInput(inputLine);
			if (saleLine != null)
			{
				SaleLines.Add(saleLine);
				Tax += saleLine.Tax;
				TotalValue += saleLine.LineValue;
				success = true;
			}

			return success;
		}
	}
}