using SOLID.Interface_Segregation;
using SOLID.Liskov;

Console.WriteLine("=== ISP Don't: fat interface ===");
IMultiFunctionDevice oldPrinter = new OldPrinter();
oldPrinter.Print("Invoice");
try
{
    oldPrinter.Scan("Invoice"); // forced member — fails at runtime
}
catch (NotSupportedException ex)
{
    Console.WriteLine($"Expected failure: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== ISP Do: depend only on IPrinter ===");
// PrintService never knows about Scan/Fax.
new PrintService(new SimplePrinter()).Run("Invoice");
new PrintService(new Photocopier()).Run("Invoice");
Console.WriteLine();

Console.WriteLine("=== LSP: shapes are substitutable ===");
var shapes = new Shape[]
{
    new Rectangle { Width = 3, Height = 4 },
    new Square { Side = 5 }
};
Console.WriteLine($"Total area: {new AreaCalculator().TotalArea(shapes)}"); // 12 + 25 = 37
Console.WriteLine();

Console.WriteLine("=== LSP: animals through Habitat ===");
var habitat = new Habitat();
habitat.MakeAnimalMove(new Animal());
habitat.MakeAnimalMove(new Fish());
habitat.MakeAnimalMove(new Bird());
