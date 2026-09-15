using Microsoft.Extensions.Options;

namespace Dependency.Implementation.Options
{
    /// <summary>
    /// Prefer Options consumer — depends on <see cref="IOptions{PricingOptions}"/>, not static fields.
    /// <para>
    /// <see cref="IOptions{T}.Value"/> is the snapshot registered at startup (singleton options).
    /// The service never reads <c>ConfigurationManager</c> or <c>StaticPricingConfig</c> itself —
    /// composition root owns binding; this class owns business use of the values.
    /// </para>
    /// </summary>
    public sealed class PricingService
    {
        private readonly PricingOptions _options;

        public PricingService(IOptions<PricingOptions> options)
        {
            // .Value is resolved from the options monitor; fail fast if misconfigured.
            _options = options.Value;
        }

        public decimal ApplyTax(decimal amount)
        {
            var withTax = amount * (1 + (decimal)_options.Rate);
            Console.WriteLine(
                $"PricingService (IOptions): {amount} + {_options.Rate:P0} tax = {withTax} {_options.Currency}");
            return withTax;
        }
    }

    /// <summary>
    /// Avoid contrast — reads a mutable static. Hard to test, easy to change from anywhere,
    /// no validation hook at startup. Prefer <see cref="PricingService"/> + <see cref="IOptions{T}"/>.
    /// </summary>
    public sealed class PricingServiceAvoid
    {
        public decimal ApplyTax(decimal amount)
        {
            var withTax = amount * (1 + (decimal)StaticPricingConfig.Rate);
            Console.WriteLine(
                $"PricingServiceAvoid (static): {amount} + {StaticPricingConfig.Rate:P0} tax = {withTax} {StaticPricingConfig.Currency}");
            return withTax;
        }
    }

    /// <summary>
    /// Avoid — global mutable config. Any code can set <see cref="Rate"/>; nothing validates at start.
    /// </summary>
    public static class StaticPricingConfig
    {
        public static double Rate { get; set; } = 0.20;
        public static string Currency { get; set; } = "EUR";
    }
}
