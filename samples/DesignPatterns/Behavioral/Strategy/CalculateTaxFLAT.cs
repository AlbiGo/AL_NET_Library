namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>Concrete strategy: flat tax.</summary>
    public class CalculateTaxFLAT : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax FLAT");
    }
}
