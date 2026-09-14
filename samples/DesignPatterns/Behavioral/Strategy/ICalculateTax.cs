namespace DesignPatterns.Behavioral.Strategy
{
    /// <summary>Strategy contract — interchangeable tax algorithms.</summary>
    public interface ICalculateTax
    {
        void Calculate();
    }
}
