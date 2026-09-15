namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Strategy context — holds an algorithm behind <see cref="ICalculateTax"/>.
    /// <para>
    /// This class is not the “work” (PERC/FLAT/PROG are). Change behavior by swapping the strategy
    /// via the constructor or <see cref="SetStrategy"/> — never by editing <see cref="Calculate"/>.
    /// Same idea as <c>ServiceGarage</c> invoking a delegate: the caller plugs in what to do.
    /// </para>
    /// </summary>
    public class TaxCalculateContext
    {
        private ICalculateTax _calculateTax;

        public TaxCalculateContext(ICalculateTax calculateTax) => _calculateTax = calculateTax;

        /// <summary>Optional: change algorithm at runtime without rebuilding the context.</summary>
        public void SetStrategy(ICalculateTax calculateTax) => _calculateTax = calculateTax;

        public void Calculate() => _calculateTax.Calculate();
    }
}
