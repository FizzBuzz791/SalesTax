namespace SalesTax
{
	public class BasicPrinter : IPrinter
	{
		public string PrintReceipt(Sale sale)
		{
			return sale.ToString();
		}
	}
}