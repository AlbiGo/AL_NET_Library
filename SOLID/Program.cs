using SOLID.Interface_Segregation;
using SOLID.Liskov;

Console.WriteLine("=== ISP: call only the surface you need ===");
// Prefer small interfaces. Callers depend on IBaseClassA or IBaseClassB, not a fat combined API.
IBaseClassA onlyA = new BaseClass();
onlyA.MethodA();

IBaseClassB onlyB = new BaseClass();
onlyB.MethodB();
Console.WriteLine();

Console.WriteLine("=== LSP: subtypes work through the base API ===");
// Habitat depends on Animal. Bird/Fish must remain substitutable without breaking callers.
var habitat = new Habitat();
habitat.MakeAnimalMove(new Animal());
habitat.MakeAnimalMove(new Fish());
habitat.MakeAnimalMove(new Bird());
