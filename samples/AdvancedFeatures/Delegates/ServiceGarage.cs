namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Core idea of a delegate: a type-safe callback.
    /// ServiceGarage does not hard-code which services run — the caller passes a method
    /// (or a chain of methods) matching <see cref="CarServiceDelegate"/>.
    /// </summary>
    public class ServiceGarage
    {
        /// <summary>
        /// Named delegate = "any method with signature void Method()".
        /// Equivalent built-in type: Action.
        /// </summary>
        public delegate void CarServiceDelegate();

        private readonly Car _car;

        public ServiceGarage(Car car)
        {
            _car = car;
        }

        /// <summary>
        /// Runs the caller's pipeline, then delivers the car.
        /// Multicast: if the delegate was built with +=, every method runs in subscription order.
        /// </summary>
        public void DoService(CarServiceDelegate? carServiceDelegate)
        {
            // Null-conditional: fine if nobody subscribed.
            carServiceDelegate?.Invoke();

            _car.Deliver();
        }
    }
}
