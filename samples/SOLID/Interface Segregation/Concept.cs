namespace SOLID.Interface_Segregation
{
    // --- Don't: one fat interface forces unused members ---

    /// <summary>
    /// Fat interface (Avoid) — a print-only device must still “implement” Scan/Fax.
    /// That is the ISP violation this sample exists to show.
    /// </summary>
    public interface IMultiFunctionDevice
    {
        void Print(string document);
        void Scan(string document);
        void Fax(string document);
    }

    /// <summary>
    /// Forced to throw for features it does not support — symptom of a fat interface.
    /// </summary>
    public class OldPrinter : IMultiFunctionDevice
    {
        public void Print(string document) => Console.WriteLine($"OldPrinter: printed '{document}'");

        public void Scan(string document) =>
            throw new NotSupportedException("OldPrinter cannot scan.");

        public void Fax(string document) =>
            throw new NotSupportedException("OldPrinter cannot fax.");
    }

    // --- Do: split roles so callers take only what they need ---

    /// <summary>Small role — print only.</summary>
    public interface IPrinter
    {
        void Print(string document);
    }

    /// <summary>Small role — scan only. Implement when you need it; skip when you do not.</summary>
    public interface IScanner
    {
        void Scan(string document);
    }

    public class SimplePrinter : IPrinter
    {
        public void Print(string document) => Console.WriteLine($"SimplePrinter: printed '{document}'");
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(string document) => Console.WriteLine($"Photocopier: printed '{document}'");

        public void Scan(string document) => Console.WriteLine($"Photocopier: scanned '{document}'");
    }

    /// <summary>
    /// Prefer ISP consumer — depends only on <see cref="IPrinter"/>.
    /// <para>
    /// Works with <see cref="SimplePrinter"/> or <see cref="Photocopier"/> without knowing about Scan/Fax.
    /// Same idea as asking for an abstraction in a ctor: take the smallest surface you need.
    /// </para>
    /// </summary>
    public class PrintService
    {
        private readonly IPrinter _printer;

        public PrintService(IPrinter printer) => _printer = printer;

        public void Run(string document) => _printer.Print(document);
    }
}
