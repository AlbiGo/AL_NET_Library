using DesignPatterns.Behavioral.Strategy;
using DesignPatterns.Creational.Abstract_Factory;
using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Factory_Method;
using DesignPatterns.Creational.Singleton;

// Comment out any demo you do not want to run.
RunStrategyDemo();
RunBuilderDemo();
RunSingletonDemo();
RunFactoryMethodDemo();
RunAbstractFactoryDemo();

static void RunStrategyDemo()
{
    Console.WriteLine("=== Strategy ===");
    var taxServiceContext = new TaxCalculateContext(new CalculateTaxPERC());
    taxServiceContext.Calculate();
    Console.WriteLine();
}

static void RunBuilderDemo()
{
    Console.WriteLine("=== Builder ===");
    var shop = new Shop();
    var carBuilder = new CarBuilder();
    shop.Construct(carBuilder);
    var car = carBuilder.GetVehicle();
    car.PrintParts();
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

    // Concurrent callers still share one instance (double-checked locking).
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
    var world = new AnimalWorld(new HerbivoreFactory(), new CarnivoreFactory());
    world.RunFoodChain();
    Console.WriteLine();
}
