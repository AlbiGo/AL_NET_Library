namespace DesignPatterns.Creational.Singleton
{
    /// <summary>
    /// Thread-safe Singleton using double-checked locking.
    /// Value is set only when the instance is first created.
    /// </summary>
    public class Singleton
    {
        private static Singleton? _instance;
        private static readonly object _lockObject = new object();

        private Singleton() { }

        public string? Value { get; private set; }

        /// <summary>
        /// Returns the single shared instance. The <paramref name="value"/>
        /// argument is applied only on first creation; later calls ignore it.
        /// </summary>
        public static Singleton GetInstance(string value)
        {
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton { Value = value };
                    }
                }
            }

            return _instance;
        }
    }
}
