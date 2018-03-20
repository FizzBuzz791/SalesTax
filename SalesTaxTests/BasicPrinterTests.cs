using NUnit.Framework;
using SalesTax.Models;
using SalesTax.Printers;

namespace SalesTaxTests
{
	[TestFixture]
	public class BasicPrinterTests
	{
		// Bad input
		[TestCase(new[] {""}, "Sales Taxes: 0.00\nTotal: 0.00", TestName = "Empty String (Test 6)", Category = "Bad Input")] // Test 6
		[TestCase(new[] {"js s jss s"}, "Sales Taxes: 0.00\nTotal: 0.00", TestName = "Nonsense String (Test 5)", Category = "Bad Input")] // Test 5
		[TestCase(new[] {"1 book: 12.49"}, "Sales Taxes: 0.00\nTotal: 0.00", TestName = "Wrong Format", Category = "Bad Input")]
		// No tax
		[TestCase(new[] {"1 book at 12.49"}, "1 book: 12.49\nSales Taxes: 0.00\nTotal: 12.50", TestName = "1 Book", Category = "No Tax")]
		[TestCase(new[] {"1 packet of headache tablets at 9.75"}, "1 packet of headache tablets: 9.75\nSales Taxes: 0.00\nTotal: 9.75", TestName = "1 Medical Item", Category = "No Tax")]
		[TestCase(new[] {"1 book at 12.49", "1 packet of headache tablets at 9.75"}, "1 book: 12.49\n1 packet of headache tablets: 9.75\nSales Taxes: 0.00\nTotal: 22.25", TestName = "1 Book & 1 Medical Item", Category = "No Tax")]
		// 10% tax
		[TestCase(new[] {"1 music CD at 14.99"}, "1 music CD: 16.49\nSales Taxes: 1.50\nTotal: 16.50", TestName = "1 CD", Category = "10% Tax")]
		[TestCase(new[] {"1 book at 12.49", "1 music CD at 14.99", "1 packet of chips at 0.85"}, "1 book: 12.49\n1 music CD: 16.49\n1 packet of chips: 0.85\nSales Taxes: 1.50\nTotal: 29.85", TestName = "1 Book, 1 CD & 1 Packet of Chips (Test 1)", Category = "10% Tax")] // Test 1
		// 5%/15% tax (imported)
		[TestCase(new[] {"1 imported box of chips at 10.00"}, "1 imported box of chips: 10.50\nSales Taxes: 0.50\nTotal: 10.50", TestName = "1 Food Item", Category = "Imported")]
		[TestCase(new[] {"1 imported transformer at 47.50"}, "1 imported transformer: 54.62\nSales Taxes: 7.12\nTotal: 54.65", TestName = "1 General Item", Category = "Imported")]
		[TestCase(new[] {"1 imported box of chips at 10.00", "1 imported transformer at 47.50"}, "1 imported box of chips: 10.50\n1 imported transformer: 54.62\nSales Taxes: 7.62\nTotal: 65.15", TestName = "1 General Item & 1 Box of Chips (Test 2)", Category = "Imported")] // Test 2
		// Mixed
		[TestCase(new[] {"1 barrell of imported oil at 27.99", "1 bottle of perfume at 18.99", "1 packet of headache tablets at 9.75", "1 box of imported chocolates at 11.25"}, "1 imported barrell of oil: 32.19\n1 bottle of perfume: 20.89\n1 packet of headache tablets: 9.75\n1 imported box of chocolates: 11.81\nSales Taxes: 6.66\nTotal: 74.65", TestName = "1 Barrell of Oil, 1 Bottle of Perfume, 1 Packet of Headache Tablets & 1 Box of Chocolates (Test 3)", Category = "Mixed")] // Test 3
		[TestCase(new[] {"10 imported bottles of whiskey at 27.99", "10 bottles of local whiskey at 18.99", "10 packets of iodine tablets at 9.75", "10 boxes of imported potato chips at 11.25"}, "10 imported bottles of whiskey: 321.88\n10 bottles of local whiskey: 208.89\n10 packets of iodine tablets: 97.50\n10 imported boxes of potato chips: 118.12\nSales Taxes: 66.59\nTotal: 746.40", TestName = "10 Bottles of Whiskey, 10 Bottles of Local Whiskey, 10 Packets of Iodine Tablets & 10 Boxes of Potato Chips (Test 4)", Category = "Mixed")] // Test 4
		public void PrintReceipt_OutputsExpectedResult(string[] input, string expectedResult)
		{
			// Arrange
			Sale sale = new Sale();
			IPrinter printer = new BasicPrinter();

			// Act
			foreach (string item in input)
			{
				sale.Add(item);
			}

			// Assert
			Assert.That(printer.PrintReceipt(sale), Is.EqualTo(expectedResult));
		}
	}
}