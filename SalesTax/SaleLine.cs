using System;

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
            int taxRate;
            if (string.IsNullOrEmpty(name)) name = string.Empty;

            Quantity = lineQuantity;
            ProductName = name;
            Price = unitPrice;
            IsImported = itemIsImported;
            LineValue = Price * Quantity;

            // calculate taxable amount
            // ideally should really have a product list and tax rules, but this'll have to do for the exercise.
            if (ProductName.Contains("book") || ProductName.Contains("tablet") || ProductName.Contains("chip"))
                taxRate = 0;  //No base tax applicable for books, medicals items or food
            else
                taxRate = 10; //10% base tax or general products

            if (IsImported)
                taxRate = 5; //5% regardless for any imported items

            Tax = CalculateTax(LineValue,taxRate);
            //Add tax to line value
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
	        double amount = (double)Math.Round(value * taxRate/100,2);

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
            return $"{Quantity} {ProductName}: {LineValue:#,###.00}";
        }
    }
}