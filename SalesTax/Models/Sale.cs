using System.Collections.Generic;
using System.Linq;

namespace SalesTax.Models
{
	public class Sale
	{
		public List<SaleLine> SaleLines { get; }

		/// <summary>
		/// The total Tax amount for the sale
		/// </summary>
		public decimal Tax
		{
			get { return SaleLines.Sum(s => s.Tax); }
		}

		/// <summary>
		/// The total value of the sale (including Tax)
		/// </summary>
		public decimal TotalValue
		{
			get
			{
				double total = (double) SaleLines.Sum(s => s.LineValue + s.Tax);

				//Now round up to nearest 5 cents.
				double remainder = total % .05;
				if (remainder > 0)
					total += .05 - remainder;

				return (decimal)total;
			}
		}

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
				success = true;
			}

			return success;
		}
	}
}