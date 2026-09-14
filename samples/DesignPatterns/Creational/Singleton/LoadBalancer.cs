namespace DesignPatterns.Creational.Singleton
{
    /// <summary>
    /// Eager Singleton: instance is created when the type loads.
    /// CLR type initialization is thread-safe, so no explicit lock is required.
    /// Contrast with <see cref="Singleton"/> which creates on first GetInstance (lazy).
    /// </summary>
    public class LoadBalancer
    {
        private static readonly LoadBalancer Instance = new LoadBalancer();
        private readonly List<Server> _servers;
        private readonly Random _random = new Random();

        private LoadBalancer()
        {
            _servers = new List<Server>
            {
                new Server { Name = "ServerI", IP = "120.14.220.18" },
                new Server { Name = "ServerII", IP = "120.14.220.19" },
                new Server { Name = "ServerIII", IP = "120.14.220.20" },
                new Server { Name = "ServerIV", IP = "120.14.220.21" },
                new Server { Name = "ServerV", IP = "120.14.220.22" },
            };
        }

        public static LoadBalancer GetLoadBalancer() => Instance;

        /// <summary>Picks a random server from the shared list (demo of shared state).</summary>
        public Server Server
        {
            get
            {
                int r = _random.Next(_servers.Count);
                return _servers[r];
            }
        }
    }

    public class Server
    {
        public string? Name { get; set; }
        public string? IP { get; set; }
        public bool Taken { get; set; }
    }
}
