using Dependency.Implementation.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dependency.Implementation
{
    /// <summary>
    /// Composition root for the Dependency sample — not a business class.
    /// <para>
    /// Rules demonstrated: (1) register interface → implementation once,
    /// (2) build a single <see cref="ServiceProvider"/> and reuse it,
    /// (3) create scopes when resolving scoped services (DbContext-style lifetimes),
    /// (4) Prefer Options — bind <see cref="PricingOptions"/> from <c>appsettings.json</c>,
    /// validate, inject <c>IOptions&lt;T&gt;</c>.
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

                // Prefer: settings live in JSON; composition root loads and binds them.
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();

                var services = new ServiceCollection();

                services.AddScoped<MathDBContext>();
                services.AddScoped<IMathRepo, MathRepo>();
                services.AddScoped<IMathService, MathService>();
                services.AddTransient<EconomicsController>();

                // Bind "Pricing" section → PricingOptions; validate before the app runs.
                services.AddOptions<PricingOptions>()
                    .Bind(configuration.GetSection("Pricing"))
                    .Validate(o => o.Rate is > 0 and <= 1.0, "PricingOptions.Rate must be in (0, 1].")
                    .Validate(o => !string.IsNullOrWhiteSpace(o.Currency), "PricingOptions.Currency is required.")
                    .ValidateOnStart();

                services.AddTransient<PricingService>();

                configure?.Invoke(services);

                _provider = services.BuildServiceProvider();

                // Surface validation errors immediately in this console sample.
                _ = _provider.GetRequiredService<IOptions<PricingOptions>>().Value;
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
