using AdvancedFeatures.Delegates;
using AdvancedFeatures.Events;
using AdvancedFeatures.Expressions;
using AdvancedFeatures.Generics.Implementation;
using AdvancedFeatures.Generics.Implementation.Data;
using AdvancedFeatures.Linq;

/// <summary>
/// Extra garage step from another class — still attachable to CarServiceDelegate
/// because TransmissionService has signature void ().
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
    }

    private static void RunDelegatesDemo()
    {
        Console.WriteLine("=== Delegates ===");
        // Multicast pipeline: += adds methods; Invoke runs them in order; then Deliver().
        var car = new Car { Name = "Evo" };
        var services = new CarServicesLocal(car);
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
        // Publisher raises; many subscribers handle. Only TaskService can Invoke the event.
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
        // AsQueryable + InlineFilter keeps Expression trees (SQL-translatable style).
        // Do not .Compile() before Where on IQueryable.
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
        // GenericServices&lt;T&gt; calls data.Calculate() — Data1/Data2 supply the math.
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
        // WherePositive uses yield return (deferred until foreach).
        var numbers = new List<int> { 1, -11, -2, 4 };
        foreach (var n in numbers.WherePositive())
        {
            Console.WriteLine(n);
        }
    }
}
