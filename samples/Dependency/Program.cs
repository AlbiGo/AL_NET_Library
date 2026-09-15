using Dependency.Concept;
using Dependency.Implementation;
using Dependency.Implementation.Options;
using Microsoft.Extensions.DependencyInjection;
using Math = Dependency.Implementation.Math;

Console.WriteLine("=== Dependency Injection (Prefer) ===");
Console.WriteLine("Composition root registers services once; controller gets IMathService via ctor.");
Console.WriteLine();

// 1) Composition root — register + build once (includes PricingOptions).
AppServices.Configure();

// 2) Scope — required for scoped MathDBContext / repos.
using (var scope = AppServices.CreateScope())
{
    // 3) Resolve controller; container injects IMathService automatically.
    var controller = scope.ServiceProvider.GetRequiredService<EconomicsController>();
    controller.EconomicsCalc(new Math { Value = 42 });
}

Console.WriteLine();
Console.WriteLine("=== Manual wiring contrast (Avoid for large graphs) ===");
var manual = new EconomicsControllerV2();
manual.EconomicsCalc(new Math { Value = 7 });

Console.WriteLine();
Console.WriteLine("=== Options pattern (Prefer IOptions<T> from appsettings.json) ===");
// Composition root loaded Pricing from JSON; PricingService only sees IOptions.
using (var scope = AppServices.CreateScope())
{
    var pricing = scope.ServiceProvider.GetRequiredService<PricingService>();
    pricing.ApplyTax(100m);
}

Console.WriteLine();
Console.WriteLine("=== Options contrast (Avoid static config) ===");
StaticPricingConfig.Rate = 0.20;
new PricingServiceAvoid().ApplyTax(100m);
// Anyone can mutate this — no startup validation, hard to test in isolation.
StaticPricingConfig.Rate = 0.99;
Console.WriteLine($"(static was mutated to {StaticPricingConfig.Rate:P0} from anywhere)");

Console.WriteLine();
Console.WriteLine("=== Constructor injection without a container (Concept) ===");
var classC = new ClassC(new ClassA(new ClassB(), new ClassB2()));
classC.MethodA();
classC.Method2();
