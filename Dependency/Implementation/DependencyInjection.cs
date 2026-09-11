using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Implementation
{
    /// <summary>
    /// Composition root: registers services once and reuses a single ServiceProvider.
    /// </summary>
    public static class AppServices
    {
        private static readonly object Sync = new object();
        private static ServiceProvider? _provider;

        public static void Configure(Action<IServiceCollection>? configure = null)
        {
            lock (Sync)
            {
                if (_provider != null)
                {
                    return;
                }

                var services = new ServiceCollection();
                services.AddScoped<MathDBContext>();
                services.AddScoped<IMathRepo, MathRepo>();
                services.AddScoped<IMathService, MathService>();
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
        /// Creates a scope for resolving scoped services (DbContext, repos, etc.).
        /// Dispose the scope when the unit of work is finished.
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
