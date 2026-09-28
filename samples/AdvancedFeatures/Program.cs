using AdvancedFeatures.Delegates;
using AdvancedFeatures.Events;
using AdvancedFeatures.Expressions;
using AdvancedFeatures.Generics.Implementation;
using AdvancedFeatures.Generics.Implementation.Data;
using AdvancedFeatures.Linq;
using AdvancedFeatures.Reflection;

/// <summary>
/// Extra garage step from another class — still attachable to <c>CarServiceDelegate</c> because
/// <see cref="TransmissionService" /> has signature <c>void ()</c>.
/// <para>
/// Proves delegates care about shape, not hierarchy: this type sits next to <see
/// cref="AdvancedFeatures.Delegates.CarServices" /> in the same multicast pipeline.
/// </para>
/// </summary>
public class CarServiceExtension
{
    private readonly Car _car;

    public CarServiceExtension(Car car) => _car = car;

    public void TransmissionService() => Console.WriteLine($"{_car.Name}: transmission serviced");
}

internal class Program
{
    private static void Main()
    {
        RunDelegatesDemo();
        RunEventsDemo();
        RunExpressionTreesDemo();
        RunGenericsDemo();
        RunLinqDemo();
        RunReflectionDemo();
    }

    private static void RunDelegatesDemo()
    {
        Console.WriteLine("=== Delegates ===");
        var car = new Car { Name = "Evo" };
        var services = new CarServices(car);
        var garage = new ServiceGarage(car);
        var extension = new CarServiceExtension(car);

        ServiceGarage.CarServiceDelegate pipeline = services.EngineService;
        pipeline += services.TireChange;
        pipeline += services.OilChange;
        pipeline += extension.TransmissionService;

        garage.DoService(pipeline);
        Console.WriteLine();
    }

    private static void RunEventsDemo()
    {
        Console.WriteLine("=== Events ===");
        var taskService = new TaskService();
        var app = new AppService();
        var email = new EmailService();

        taskService.TaskCreated += app.OnTaskCreated;
        taskService.TaskCreated += email.OnTaskCreated;
        taskService.TaskCompleted += app.OnTaskCompleted;
        taskService.TaskCompleted += email.OnTaskCompleted;

        var work = new TaskItem { Title = "Write stored procedure" };
        taskService.PrepareTask(work);
        taskService.CompleteTask(work);
        Console.WriteLine();
    }

    private static void RunExpressionTreesDemo()
    {
        Console.WriteLine("=== Expression trees ===");
        var students = new List<Student>
        {
            new() { StudentID = 1, StudentName = "Albus", Age = 23, Email = "Albus@gmail.com" },
            new() { StudentID = 2, StudentName = "Donus", Age = 13, Email = "Donus@gmail.com" },
            new() { StudentID = 3, StudentName = "Xhonus", Age = 45, Email = "Xhonus@gmail.com" }
        }.AsQueryable();

        var filter = new StudentFilter { Age = 20, Email = "Xhonus" };
        foreach (var student in students.InlineFilter(filter))
        {
            Console.WriteLine($"{student.StudentName} ({student.Age}) {student.Email}");
        }

        Console.WriteLine();
    }

    private static void RunGenericsDemo()
    {
        Console.WriteLine("=== Generics ===");
        var data1 = new Data1();
        var data2 = new Data2();
        GenericServices<Data1>.Calculate(data1);
        GenericServices<Data2>.Calculate(data2);
        Console.WriteLine($"Data1 MainEconomics = {data1.MainEconomics}");
        Console.WriteLine($"Data2 MainEconomics = {data2.MainEconomics}");
        Console.WriteLine();
    }

    private static void RunLinqDemo()
    {
        Console.WriteLine("=== Custom LINQ ===");
        var numbers = new List<int> { 1, -11, -2, 4 };
        foreach (var n in numbers.WherePositive())
        {
            Console.WriteLine(n);
        }

        Console.WriteLine();
    }

    private static void RunReflectionDemo()
    {
        Console.WriteLine("=== Reflection ===");
        // Prefer: discover [Plugin] + IPlugin — Main never new HelloPlugin().
        foreach (var plugin in PluginScanner.Discover(typeof(PluginScanner).Assembly))
        {
            Console.WriteLine(plugin.Describe());
        }

        Console.WriteLine();
        // Prefer direct / cached reflection; Avoid uncached magic strings.
        var person = new Person { Name = "Ada", Age = 36 };
        Console.WriteLine($"Direct:            {PropertyAccessDemo.PreferDirect(person)}");
        Console.WriteLine($"Cached reflection: {PropertyAccessDemo.PreferCachedReflection(person)}");
        Console.WriteLine($"Avoid (uncached):  {PropertyAccessDemo.AvoidUncachedMagicString(person)}");

        Console.WriteLine();
        Console.WriteLine("--- Dynamic JSON (shape always different) ---");
        // Load real files — each payload has a different shape; only the property name is known.
        var reflectionDir = Path.Combine(AppContext.BaseDirectory, "Reflection");
        var orderJson = File.ReadAllText(Path.Combine(reflectionDir, "sample-order.json"));
        var userJson = File.ReadAllText(Path.Combine(reflectionDir, "sample-user.json"));
        var sensorJson = File.ReadAllText(Path.Combine(reflectionDir, "sample-sensor.json"));
        var itemsJson = File.ReadAllText(Path.Combine(reflectionDir, "sample-items.json"));

        Console.WriteLine($"JsonNode orderId:   {DynamicJsonLookup.GetByPropertyName(orderJson, "orderId")}");
        Console.WriteLine($"JsonNode email:     {DynamicJsonLookup.GetByPropertyName(userJson, "email")}");
        Console.WriteLine($"JsonElement celsius:{DynamicJsonLookup.GetWithJsonElement(sensorJson, "celsius")}");

        // List of objects — same property name on each element; missing keys → null.
        Console.WriteLine("List → type:");
        var types = DynamicJsonLookup.GetByPropertyNameFromEach(itemsJson, "type");
        for (var i = 0; i < types.Count; i++)
            Console.WriteLine($"  [{i}] type={types[i]}");

        Console.WriteLine("List → email (only user rows have it):");
        var emails = DynamicJsonLookup.GetByPropertyNameFromEach(itemsJson, "email");
        for (var i = 0; i < emails.Count; i++)

            Console.WriteLine($"  [{i}] email={emails[i] ?? "(missing)"}");

        // property + value → matching object
        Console.WriteLine("Get object where email=ada@example.com:");
        Console.WriteLine($"  {DynamicJsonLookup.GetObjectByPropertyValue(itemsJson, "email", "ada@example.com")}");

        // Contrast: Deserialize<object> + GetProperty("email") asks the wrong question.
        DynamicJsonLookup.ClrGetPropertyOnJson(userJson, "email");
        Console.WriteLine($"  Same key via JsonNode: {DynamicJsonLookup.GetByPropertyName(userJson, "email")}");
    }
}
