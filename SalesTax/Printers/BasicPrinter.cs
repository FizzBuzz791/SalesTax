using System.Text;
using SalesTax.Models;

namespace SalesTax.Printers
{
	public class BasicPrinter : IPrinter
	{
		public string PrintReceipt(Sale sale)
		{
			StringBuilder output = new StringBuilder();

			foreach (SaleLine line in sale.SaleLines)
			{
				if (output.Length > 0)
					output.Append("\n");

				output.Append(line);
			}

			//Now add footer information
			if (output.Length > 0)
				output.Append("\n");

			output.Append($"Sales Taxes: {sale.Tax:#,##0.00}");
			output.Append("\n");
			output.Append($"Total: {sale.TotalValue:#,##0.00}");
			return output.ToString();
		}
	}
}