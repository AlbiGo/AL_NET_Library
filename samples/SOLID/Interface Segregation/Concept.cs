namespace SOLID.Interface_Segregation
{
    // --- Don't: one fat interface forces unused members ---

    /// <summary>
    /// Fat interface — a simple printer must still "implement" Scan/Fax even if it cannot.
    /// </summary>
    public interface IMultiFunctionDevice
    {
        void Print(string document);
        void Scan(string document);
        void Fax(string document);
    }

    /// <summary>Forced to throw / no-op for features it does not support.</summary>
    public class OldPrinter : IMultiFunctionDevice
    {
        public void Print(string document) => Console.WriteLine($"OldPrinter: printed '{document}'");

        public void Scan(string document) =>
            throw new NotSupportedException("OldPrinter cannot scan.");

        public void Fax(string document) =>
            throw new NotSupportedException("OldPrinter cannot fax.");
    }

    // --- Do: split roles so callers take only what they need ---

    public interface IPrinter
    {
        void Print(string document);
    }

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

    /// <summary>Depends only on IPrinter — works with SimplePrinter or Photocopier.</summary>
    public class PrintService
    {
        private readonly IPrinter _printer;

        public PrintService(IPrinter printer) => _printer = printer;

        public void Run(string document) => _printer.Print(document);
    }
}
