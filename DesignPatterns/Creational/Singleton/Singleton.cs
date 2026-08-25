namespace DesignPatterns.Creational.Singleton
{
    public class Singleton
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Singleton() { }

        public string? Value { get; set; }

        private static readonly object _lockObject = new object();
        // The Singleton's instance is stored in a static field. There are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded programs.
        private static Singleton? _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the
        // existing object stored in the static field.
        public static Singleton GetInstance(string value)
        {
            if (_instance == null)
            {
                //lock(_lockObject)
                //{
                    _instance = new Singleton();
                    _instance.Value = value;
                //}
            }
            return _instance;
        }
    }
}
