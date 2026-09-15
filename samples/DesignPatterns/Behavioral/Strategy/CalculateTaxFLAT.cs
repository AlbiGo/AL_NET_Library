namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Concrete strategy — flat tax. Interchangeable with PERC/PROG behind <see cref="ICalculateTax"/>.
    /// </summary>
    public class CalculateTaxFLAT : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax FLAT");
    }
}
