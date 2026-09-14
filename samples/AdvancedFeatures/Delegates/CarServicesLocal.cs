namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Methods here are the "payload" of the delegate demo.
    /// Each matches CarServiceDelegate (void, no args), so any of them can be
    /// assigned or combined with += into a multicast pipeline.
    /// </summary>
    public class CarServicesLocal
    {
        private readonly Car _car;

        public CarServicesLocal(Car car) => _car = car;

        public void OilChange() => Console.WriteLine($"{_car.Name}: oil changed");

        public void TireChange() => Console.WriteLine($"{_car.Name}: tires changed");

        public void EngineService() => Console.WriteLine($"{_car.Name}: engine serviced");
    }
}
