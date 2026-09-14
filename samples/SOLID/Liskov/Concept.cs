namespace SOLID.Liskov
{
    /// <summary>
    /// LSP — subtypes must honor the base contract.
    /// Any Shape passed to AreaCalculator must return a sensible area (not throw / lie).
    /// </summary>
    public abstract class Shape
    {
        public abstract double Area();
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double Area() => Width * Height;
    }

    public class Square : Shape
    {
        public double Side { get; set; }

        public override double Area() => Side * Side;
    }

    /// <summary>Works for every Shape — Rectangle and Square are interchangeable here.</summary>
    public class AreaCalculator
    {
        public double TotalArea(IEnumerable<Shape> shapes) => shapes.Sum(s => s.Area());
    }

    // Keep the animal demo as a second, simpler LSP illustration.
    public class Animal
    {
        public virtual void Moves() => Console.WriteLine("Animal moves");
    }

    public class Bird : Animal
    {
        public override void Moves() => Console.WriteLine("Bird flies");
    }

    public class Fish : Animal
    {
        public override void Moves() => Console.WriteLine("Fish swims");
    }

    public class Habitat
    {
        public void MakeAnimalMove(Animal animal) => animal.Moves();
    }
}
