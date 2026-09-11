using Dependency.Concept;
using Dependency.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Math = Dependency.Implementation.Math;

Console.WriteLine("=== Dependency Injection ===");

// Build the container once at startup (composition root).
AppServices.Configure();

using (var scope = AppServices.CreateScope())
{
    var controller = scope.ServiceProvider.GetRequiredService<EconomicsController>();
    controller.EconomicsCalc(new Math());
}

Console.WriteLine("---------------------------------------");

// Concept demo: constructor injection without Microsoft.Extensions.DI
var classC = new ClassC(new ClassA(new ClassB(), new ClassB2()));
classC.MethodA();
classC.Method2();
