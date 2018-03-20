using System.Collections.Generic;
using System.Text;

namespace SalesTax
{
	public class Sale
	{
		private readonly List<SaleLine> _saleLines = new List<SaleLine>();

		/// <summary>
		/// The total Tax amount for the sale
		/// </summary>
		public decimal Tax { get; private set; }

		/// <summary>
		/// The total value of the sale (including Tax)
		/// </summary>
		public decimal TotalValue { get; private set; }

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
				_saleLines.Add(saleLine);
				Tax += saleLine.Tax;
				TotalValue += saleLine.LineValue;
				success = true;
			}

			return success;
		}
		
		/// <summary>
		/// Converts the sale to a string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder output = new StringBuilder();

			foreach (SaleLine line in _saleLines)
			{
				if (output.Length > 0)
					output.Append("\n");
				output.Append(line);
			}
			//Now add footer information
			output.Append("\n");
			output.Append($"Sales Taxes: {Tax:#,##0.00}");
			output.Append("\n");
			output.Append($"Total: {TotalValue:#,##0.00}");
			return output.ToString();
		}
	}
}