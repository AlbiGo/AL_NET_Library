namespace DesignPatterns.Behavioral.Strategy
{
    public class TaxCalculateContext
    {
        private readonly ICalculateTax _calculateTax;

        public TaxCalculateContext(CalculateTax calculateTax)
        {
            _calculateTax = calculateTax;
        }

        public void Calculate()
        {
            _calculateTax.Calculate();
        }
    }
}