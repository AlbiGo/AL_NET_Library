namespace Dependency.Concept
{
    public interface IClassA
    {
        void MethodA();
        void MethodA2();
    }

    public class ClassA : IClassA
    {
        private readonly IClassB _classB;
        private readonly IClassB2 _classB2;

        public ClassA(IClassB classB, IClassB2 classB2)
        {
            _classB = classB;
            _classB2 = classB2;
        }

        public void MethodA() => _classB.MethodB();
        public void MethodA2() => _classB2.MethodB2();
    }

    public interface IClassB
    {
        void MethodB();
    }

    public class ClassB : IClassB
    {
        public void MethodB() => Console.WriteLine("Method B");
    }

    public interface IClassB2
    {
        void MethodB2();
    }

    public class ClassB2 : IClassB2
    {
        public void MethodB2() => Console.WriteLine("Method B2");
    }

    public class ClassC
    {
        private readonly IClassA _classA;

        public ClassC(IClassA classA)
        {
            _classA = classA;
        }

        public void MethodA() => _classA.MethodA();
        public void Method2() => _classA.MethodA2();
    }

    /// <summary>
    /// Minimal teaching service locator. Prefer constructor injection via a real container
    /// (see <c>AppServices</c> in Dependency.Implementation) for application code.
    /// </summary>
    public static class DependencyInjectionProvider
    {
        private static readonly Dictionary<Type, Func<object>> Services = new();

        public static void Register<TService>(Func<TService> factory) where TService : class
        {
            Services[typeof(TService)] = () => factory();
        }

        public static void Register<TService, TImplementation>()
            where TImplementation : class, TService
        {
            Services[typeof(TService)] = () =>
            {
                // Prefer a parameterless constructor; otherwise resolve ctor args from the registry.
                var ctors = typeof(TImplementation).GetConstructors();
                var ctor = ctors.OrderByDescending(c => c.GetParameters().Length).First();
                var args = ctor.GetParameters()
                    .Select(p => Resolve(p.ParameterType))
                    .ToArray();
                return Activator.CreateInstance(typeof(TImplementation), args)
                       ?? throw new InvalidOperationException($"Could not create {typeof(TImplementation).Name}.");
            };
        }

        public static void Register<T>() where T : class
        {
            Register<T, T>();
        }

        public static TService Resolve<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }

        private static object Resolve(Type serviceType)
        {
            if (Services.TryGetValue(serviceType, out var factory))
            {
                return factory();
            }

            throw new InvalidOperationException($"Service of type {serviceType.Name} is not registered.");
        }
    }
}
