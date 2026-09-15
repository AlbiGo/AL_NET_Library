namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Concrete strategy — percentage tax.
    /// <para>
    /// Pluggable into <see cref="TaxCalculateContext"/> via <see cref="ICalculateTax"/>.
    /// Swappable with FLAT/PROG without editing the context (same idea as plugging methods into a delegate).
    /// </para>
    /// </summary>
    public class CalculateTaxPERC : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax PERC");
    }
}
