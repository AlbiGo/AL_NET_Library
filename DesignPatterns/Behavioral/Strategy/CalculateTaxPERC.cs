namespace DesignPatterns.Behavioral.Strategy
{
    public class CalculateTaxPERC : CalculateTax, ICalculateTax
    {
        public override void Calculate()
        {
            Console.WriteLine("Calculate tax PERC");
        }
    }
}
