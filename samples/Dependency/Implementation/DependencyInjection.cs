using Microsoft.Extensions.DependencyInjection;

namespace Dependency.Implementation
{
    /// <summary>
    /// Composition root for the Dependency sample — not a business class.
    /// <para>
    /// Rules demonstrated: (1) register interface → implementation once,
    /// (2) build a single <see cref="ServiceProvider"/> and reuse it,
    /// (3) create scopes when resolving scoped services (DbContext-style lifetimes).
    /// Controllers like <see cref="EconomicsController"/> never call this for each operation —
    /// startup configures; request/unit-of-work opens a scope.
    /// </para>
    /// </summary>
    public static class AppServices
    { 
        private static readonly object Sync = new object();
        private static ServiceProvider? _provider;

        /// <summary>Startup only. Second call is a no-op so we never rebuild the container.</summary>
        public static void Configure(Action<IServiceCollection>? configure = null)
        {
            lock (Sync)
            {
                if (_provider != null)
                    return;

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
                Configure();

            return _provider!.GetRequiredService<T>();
        }

        public static IServiceScope CreateScope()
        {
            if (_provider == null)
                Configure();

            return _provider!.CreateScope();
        }
    }
}
