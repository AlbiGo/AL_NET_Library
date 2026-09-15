namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>
    /// Concrete strategy — progressive tax. Interchangeable with PERC/FLAT behind <see cref="ICalculateTax"/>.
    /// </summary>
    public class CalculateTaxPROG : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax PROG");
    }
}
