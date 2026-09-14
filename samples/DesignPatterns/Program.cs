using DesignPatterns.Behavioral.Strategy;
using DesignPatterns.Creational.Abstract_Factory;
using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Factory_Method;
using DesignPatterns.Creational.Singleton;

// Each Run*Demo shows one pattern. Comment out any you do not want.
// Prefer / Avoid + longer walkthroughs: docs/06-solid-and-patterns.md

RunStrategyDemo();
RunBuilderDemo();
RunSingletonDemo();
RunFactoryMethodDemo();
RunAbstractFactoryDemo();

static void RunStrategyDemo()
{
    Console.WriteLine("=== Strategy ===");
    // Context depends on ICalculateTax — swap strategies without changing TaxCalculateContext.
    var tax = new TaxCalculateContext(new CalculateTaxPERC());
    tax.Calculate();

    tax.SetStrategy(new CalculateTaxFLAT());
    tax.Calculate();

    tax.SetStrategy(new CalculateTaxPROG());
    tax.Calculate();
    Console.WriteLine();
}

static void RunBuilderDemo()
{
    Console.WriteLine("=== Builder ===");
    // Same Shop steps; different builders → different products.
    var shop = new Shop();

    Console.WriteLine("Car:");
    shop.Construct(new CarBuilder()).PrintParts();

    Console.WriteLine("Motorcycle:");
    shop.Construct(new MotorcycleBuilder()).PrintParts();
    Console.WriteLine();
}

static void RunSingletonDemo()
{
    Console.WriteLine("=== Singleton ===");
    var first = Singleton.GetInstance("FOO");
    var second = Singleton.GetInstance("BAR");
    Console.WriteLine($"First value: {first.Value}");
    Console.WriteLine($"Second value: {second.Value} (second call does not overwrite)");
    Console.WriteLine($"Same instance: {ReferenceEquals(first, second)}");

    Singleton? fromThreadA = null;
    Singleton? fromThreadB = null;
    var threadA = new Thread(() => fromThreadA = Singleton.GetInstance("A"));
    var threadB = new Thread(() => fromThreadB = Singleton.GetInstance("B"));
    threadA.Start();
    threadB.Start();
    threadA.Join();
    threadB.Join();
    Console.WriteLine($"Same instance across threads: {ReferenceEquals(fromThreadA, fromThreadB)}");
    Console.WriteLine($"LoadBalancer (eager singleton) server: {LoadBalancer.GetLoadBalancer().Server.Name}");
    Console.WriteLine();
}

static void RunFactoryMethodDemo()
{
    Console.WriteLine("=== Factory Method ===");
    Document[] documents = { new Resume(), new Report() };
    foreach (var document in documents)
    {
        Console.WriteLine($"{document.GetType().Name}:");
        foreach (var page in document.pages)
        {
            Console.WriteLine($"  - {page.GetType().Name}");
        }
    }
    Console.WriteLine();
}

static void RunAbstractFactoryDemo()
{
    Console.WriteLine("=== Abstract Factory ===");
    // AnimalWorld only sees Factory — not Wolf/Bison constructors.
    var world = new AnimalWorld(new HerbivoreFactory(), new CarnivoreFactory());
    world.RunFoodChain();
    Console.WriteLine();
}
