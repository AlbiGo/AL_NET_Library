namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>Concrete strategy: progressive tax.</summary>
    public class CalculateTaxPROG : CalculateTax, ICalculateTax
    {
        public override void Calculate() => Console.WriteLine("Calculate tax prog");
    }
}
