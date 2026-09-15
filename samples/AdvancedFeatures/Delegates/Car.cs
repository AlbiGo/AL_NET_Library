namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Simple model used by the garage demo — not the focus of the lesson.
    /// <para>
    /// Service methods on <see cref="CarServices"/> print using <see cref="Name"/>.
    /// After the delegate pipeline finishes, <see cref="ServiceGarage.DoService"/> calls
    /// <see cref="Deliver"/> so you see “work done → ready for delivery.”
    /// </para>
    /// </summary>
    public class Car
    {
        public string? Name { get; set; }

        public void Deliver() => Console.WriteLine($"{Name}: all services done — ready for delivery");
    }
}
