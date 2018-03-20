using SalesTax.Models;

namespace SalesTax.Printers
{
	public interface IPrinter
	{
		string PrintReceipt(Sale sale);
	}
}