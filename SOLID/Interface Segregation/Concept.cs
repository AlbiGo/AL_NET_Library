namespace SOLID.Interface_Segregation
{
    /// <summary>
    /// Interface Segregation: split roles so callers are not forced to depend on unused members.
    /// </summary>
    public interface IBaseClassA
    {
        void MethodA();
    }

    public interface IBaseClassB
    {
        void MethodB();
    }

    public class BaseClass : IBaseClassA, IBaseClassB
    {
        public void MethodA() => Console.WriteLine("Method A");

        public void MethodB() => Console.WriteLine("Method B");
    }
}
