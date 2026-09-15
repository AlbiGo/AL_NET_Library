namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Concrete “work” for the delegate demo — not the smart orchestration.
    /// Holds one <see cref="Car"/> and exposes service steps that match
    /// <c>CarServiceDelegate</c> (<c>void</c>, no args), so any of them can be
    /// assigned or combined with <c>+=</c> into a multicast pipeline.
    /// <para>
    /// Delegates care about signature shape, not class hierarchy: these methods
    /// can sit next to steps from other types (e.g. <c>CarServiceExtension</c>).
    /// <see cref="ServiceGarage.DoService"/> never calls them by name — it
    /// invokes the pipeline, then delivers the car.
    /// </para>
    /// </summary>
    public class CarServices
    {
        private readonly Car _car;

        public CarServices(Car car) => _car = car;

        public void OilChange() => Console.WriteLine($"{_car.Name}: oil changed");

        public void TireChange() => Console.WriteLine($"{_car.Name}: tires changed");

        public void EngineService() => Console.WriteLine($"{_car.Name}: engine serviced");
    }
}
