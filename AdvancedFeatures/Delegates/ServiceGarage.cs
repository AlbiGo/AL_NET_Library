namespace AdvancedFeatures.Delegates
{
    /// <summary>
    /// Shows the core idea of a <b>delegate</b>: a type-safe callback.
    /// The garage does not know which services will run — the caller passes a method
    /// (or a chain of methods) that matches <see cref="CarServiceDelegate"/>.
    /// </summary>
    public class ServiceGarage
    {
        /// <summary>
        /// Named delegate type = "a method that takes no args and returns void".
        /// In modern C# you often write the same thing as <see cref="Action"/>,
        /// but a named delegate makes teaching samples clearer.
        /// </summary>
        public delegate void CarServiceDelegate();

        private readonly Car _car;

        public ServiceGarage(Car car)
        {
            _car = car;
        }

        /// <summary>
        /// Invokes whatever service pipeline the caller supplied, then delivers the car.
        /// If the delegate is multicast (<c>+=</c>), every subscribed method runs in order.
        /// </summary>
        public void DoService(CarServiceDelegate? carServiceDelegate)
        {
            // Null-conditional invoke: safe if nobody subscribed.
            carServiceDelegate?.Invoke();

            _car.Deliver();
        }
    }
}
