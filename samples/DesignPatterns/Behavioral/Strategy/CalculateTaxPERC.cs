namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>Concrete strategy: percentage tax.</summary>
    public class CalculateTaxPERC : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax PERC");
    }
}
