namespace DesignPatterns.Behavioral.Strategy
{
    public class CalculateTaxPROG : CalculateTax, ICalculateTax
    {
        public override void Calculate()
        {
            Console.WriteLine("Calculate tax prog");
        }
    }
}
