namespace DesignPatterns.Creational.Singleton
{
    /// <summary>
    /// Lazy, thread-safe Singleton via double-checked locking.
    /// One shared instance for the whole process; Value is set only on first create.
    /// </summary>
    public class Singleton
    {
        private static Singleton? _instance;
        private static readonly object _lockObject = new object();

        // Clients cannot call new Singleton() — forces GetInstance.
        private Singleton() { }

        public string? Value { get; private set; }

        /// <summary>
        /// Returns the single instance.
        /// <paramref name="value"/> is applied only when the instance is first created;
        /// later calls ignore it (same instance, original Value).
        /// </summary>
        public static Singleton GetInstance(string value)
        {
            // Fast path: already created — no lock.
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    // Second check: another thread may have created it while we waited.
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
