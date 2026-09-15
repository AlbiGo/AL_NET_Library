namespace DesignPatterns.Creational.Singleton
{
    /// <summary>
    /// Lazy, thread-safe Singleton via double-checked locking.
    /// <para>
    /// One shared instance for the whole process. <see cref="Value"/> is set <b>only on first create</b> —
    /// later <c>GetInstance("BAR")</c> still returns the original value. That proves the classic
    /// “init once” rule. Prefer DI lifetimes when you already have a container; use sparingly.
    /// See also eager <c>LoadBalancer</c> (no lock — type init is thread-safe).
    /// </para>
    /// </summary>
    public class Singleton
    {
        private static Singleton? _instance;
        private static readonly object _lockObject = new object();

        private Singleton() { }

        public string? Value { get; private set; }

        /// <summary>
        /// Returns the single instance.
        /// <paramref name="value"/> applies only when the instance is first created; later calls ignore it.
        /// </summary>
        public static Singleton GetInstance(string value)
        {
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                        _instance = new Singleton { Value = value };
                }
            }

            return _instance;
        }
    }
}
