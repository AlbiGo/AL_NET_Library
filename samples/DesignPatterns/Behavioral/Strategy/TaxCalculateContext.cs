namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Context holds a strategy behind <see cref="ICalculateTax"/>.
    /// Change behavior by swapping the strategy — not by editing this class.
    /// </summary>
    public class TaxCalculateContext
    {
        private ICalculateTax _calculateTax;

        public TaxCalculateContext(ICalculateTax calculateTax)
        {
            _calculateTax = calculateTax;
        }

        /// <summary>Optional: change algorithm at runtime.</summary>
        public void SetStrategy(ICalculateTax calculateTax) => _calculateTax = calculateTax;

        public void Calculate() => _calculateTax.Calculate();
    }
}
