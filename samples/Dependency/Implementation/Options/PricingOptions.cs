namespace Dependency.Implementation.Options
{
    /// <summary>
    /// Strongly typed settings — Prefer bind once at the composition root, inject via <c>IOptions&lt;T&gt;</c>.
    /// <para>
    /// Why Options (not a static <c>Config.Rate</c>)? Testability, one place to validate, and the same
    /// DI graph as the rest of the app. Values are loaded from <c>appsettings.json</c>
    /// (<c>Pricing</c> section) via <c>Bind(configuration.GetSection("Pricing"))</c>.
    /// </para>
    /// </summary>
    public sealed class PricingOptions
    {
        /// <summary>Fractional tax rate, e.g. 0.20 = 20%.</summary>
        public double Rate { get; set; }

        public string Currency { get; set; } = "EUR";
    }
}
