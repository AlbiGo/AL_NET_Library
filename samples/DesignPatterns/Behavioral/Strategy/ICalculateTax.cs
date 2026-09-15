namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Strategy contract — interchangeable tax algorithms (PERC / FLAT / PROG).
    /// <para>
    /// <see cref="TaxCalculateContext"/> depends only on this interface.
    /// Add a new tax rule by implementing this — do not edit the context.
    /// </para>
    /// </summary>
    public interface ICalculateTax
    {
        void Calculate();
    }
}
