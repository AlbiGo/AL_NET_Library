namespace DesignPatterns.Behavioral.Strategy
{
    public class CalculateTaxFLAT : CalculateTax, ICalculateTax
    {
        public override void Calculate()
        {
            Console.WriteLine("Calculate tax FLAT");
        }
    }
}
