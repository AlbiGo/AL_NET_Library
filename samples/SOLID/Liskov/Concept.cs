namespace SOLID.Liskov
{
    /// <summary>
    /// LSP base — subtypes must honor this contract.
    /// Any <see cref="Shape"/> passed to <see cref="AreaCalculator"/> must return a sensible area (not throw / lie).
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

    /// <summary>
    /// LSP check in one number — works for every <see cref="Shape"/>.
    /// <para>
    /// <see cref="Rectangle"/> and <see cref="Square"/> are interchangeable here
    /// (<c>TotalArea == 37</c> in the demo). That is the substitution rule made visible.
    /// </para>
    /// </summary>
    public class AreaCalculator
    {
        public double TotalArea(IEnumerable<Shape> shapes) => shapes.Sum(s => s.Area());
    }

    /// <summary>Second, simpler LSP story — movement through a shared API.</summary>
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

    /// <summary>
    /// Depends on <see cref="Animal"/> only — Bird/Fish must still fulfill <see cref="Animal.Moves"/>.
    /// </summary>
    public class Habitat
    {
        public void MakeAnimalMove(Animal animal) => animal.Moves();
    }
}
