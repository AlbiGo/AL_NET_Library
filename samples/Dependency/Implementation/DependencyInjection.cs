using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Implementation
{
    /// <summary>
    /// Composition root for the Dependency sample.
    ///
    /// Rules demonstrated:
    /// 1. Register mappings once (interface → implementation).
    /// 2. Build a single ServiceProvider and reuse it.
    /// 3. Create scopes when resolving scoped services (DbContext-style lifetimes).
    /// </summary>
    public static class AppServices
    {
        private static readonly object Sync = new object();
        private static ServiceProvider? _provider;

        /// <summary>
        /// Startup only. Second call is a no-op so we never rebuild the container.
        /// </summary>
        public static void Configure(Action<IServiceCollection>? configure = null)
        {
            lock (Sync)
            {
                if (_provider != null)
                {
                    return;
                }

                var services = new ServiceCollection();

                // Scoped = one instance per scope (typical for DbContext / repos).
                services.AddScoped<MathDBContext>();
                services.AddScoped<IMathRepo, MathRepo>();
                services.AddScoped<IMathService, MathService>();

                // Transient = new instance every resolve (fine for a thin controller).
                services.AddTransient<EconomicsController>();

                configure?.Invoke(services);

                _provider = services.BuildServiceProvider();
            }
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            if (_provider == null)
            {
                Configure();
            }

            return _provider!.GetRequiredService<T>();
        }

        /// <summary>
        /// Opens a DI scope. Dispose it when the unit of work ends
        /// so scoped services (and their resources) are released.
        /// </summary>
        public static IServiceScope CreateScope()
        {
            if (_provider == null)
            {
                Configure();
            }

            return _provider!.CreateScope();
        }
    }
}
