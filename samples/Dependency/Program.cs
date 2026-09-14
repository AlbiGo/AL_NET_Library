using Dependency.Concept;
using Dependency.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Math = Dependency.Implementation.Math;

Console.WriteLine("=== Dependency Injection (Prefer) ===");
Console.WriteLine("Composition root registers services once; controller gets IMathService via ctor.");
Console.WriteLine();

// 1) Composition root — register + build once.
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
// Same calculation without the container — fine for tiny demos, brittle as deps grow.
var manual = new EconomicsControllerV2();
manual.EconomicsCalc(new Math { Value = 7 });

Console.WriteLine();
Console.WriteLine("=== Constructor injection without a container (Concept) ===");
var classC = new ClassC(new ClassA(new ClassB(), new ClassB2()));
classC.MethodA();
classC.Method2();
