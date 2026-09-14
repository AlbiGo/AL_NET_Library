namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Concrete service operations. Any of these methods can be assigned to
    /// <see cref="ServiceGarage.CarServiceDelegate"/> because they match the signature
    /// <c>void Method()</c> (no parameters, no return value).
    /// </summary>
    public class CarServicesLocal
    {
        private readonly Car _car;

        public CarServicesLocal(Car car)
        {
            _car = car;
        }

        public void OilChange()
        {
            Console.WriteLine($"{_car.Name}: oil changed");
        }

        public void TireChange()
        {
            Console.WriteLine($"{_car.Name}: tires changed");
        }

        public void EngineService()
        {
            Console.WriteLine($"{_car.Name}: engine serviced");
        }
    }
}
