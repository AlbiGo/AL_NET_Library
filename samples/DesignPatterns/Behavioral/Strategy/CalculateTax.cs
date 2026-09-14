namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>Optional base for shared strategy behavior.</summary>
    public class CalculateTax : ICalculateTax
    {
        public virtual void Calculate()
        {
        }
    }
}
