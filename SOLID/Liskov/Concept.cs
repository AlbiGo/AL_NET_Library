namespace SOLID.Liskov
{
    /// <summary>
    /// Liskov Substitution: any Animal passed to Habitat must honor Moves() meaningfully.
    /// </summary>
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
