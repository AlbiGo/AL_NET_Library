namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// The “smart” half of the delegate demo: accepts a callback pipeline, does not hard-code steps.
    /// <para>
    /// <see cref="CarServices"/> holds the concrete work (<c>EngineService</c>, …).
    /// This garage only knows: invoke whatever <see cref="CarServiceDelegate"/> you pass,
    /// then <see cref="Car.Deliver"/>. Multicast <c>+=</c> runs every attached method in order.
    /// Equivalent built-in type for the delegate: <c>Action</c>.
    /// </para>
    /// </summary>
    public class ServiceGarage
    {
        /// <summary>
        /// Named delegate = any method with signature <c>void Method()</c> (same as <c>Action</c>).
        /// </summary>
        public delegate void CarServiceDelegate();

        private readonly Car _car;

        public ServiceGarage(Car car) => _car = car;

        /// <summary>
        /// Runs the caller's pipeline, then delivers the car.
        /// Does not call <c>OilChange</c> by name — only <c>pipeline?.Invoke()</c>.
        /// </summary>
        public void DoService(CarServiceDelegate? carServiceDelegate)
        {
            carServiceDelegate?.Invoke();
            _car.Deliver();
        }
    }
}
