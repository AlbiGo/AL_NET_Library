namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Simple model used by the garage demo.
    /// The garage invokes service methods via a delegate, then calls <see cref="Deliver"/>.
    /// </summary>
    public class Car
    {
        public string? Name { get; set; }

        public void Deliver()
        {
            Console.WriteLine($"{Name}: all services done — ready for delivery");
        }
    }
}
