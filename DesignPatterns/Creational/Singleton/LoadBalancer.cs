namespace DesignPatterns.Creational.Singleton
{
    public class LoadBalancer
    {
        private static LoadBalancer? instance = new LoadBalancer();
        private static readonly object locker = new object();
        private readonly List<Server> servers;
        private readonly Random random = new Random();

        private LoadBalancer()
        {
            servers = new List<Server>
            {
                new Server { Name = "ServerI", IP = "120.14.220.18" },
                new Server { Name = "ServerII", IP = "120.14.220.19" },
                new Server { Name = "ServerIII", IP = "120.14.220.20" },
                new Server { Name = "ServerIV", IP = "120.14.220.21" },
                new Server { Name = "ServerV", IP = "120.14.220.22" },
            };
        }

        public static LoadBalancer GetLoadBalancer()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new LoadBalancer();
                    }
                }
            }
            return instance;
        }

        public Server Server
        {
            get
            {
                int r = random.Next(servers.Count);
                return servers[r];
            }
        }
    }

    /// <summary>
    /// Represents a server machine
    /// </summary>
    public class Server
    {
        public string? Name { get; set; }
        public string? IP { get; set; }
        public bool Taken { get; set; }
    }
}
