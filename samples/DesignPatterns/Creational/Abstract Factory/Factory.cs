namespace DesignPatterns.Creational.Abstract_Factory
{
    /// <summary>
    /// Abstract Factory: client depends on <see cref="Factory"/>, not on Wolf/Bison constructors.
    /// </summary>
    public abstract class Factory
    {
        public abstract Animal CreateAnimal();
    }

    public class CarnivoreFactory : Factory
    {
        public override Animal CreateAnimal() => new Wolf();
    }

    public class HerbivoreFactory : Factory
    {
        public override Animal CreateAnimal() => new Bison();
    }

    public class Wolf : Animal
    {
        public Wolf() => Type = AnimalType.Carnivore;
    }

    public class Bison : Animal
    {
        public Bison() => Type = AnimalType.Herbivore;
    }

    /// <summary>
    /// Depends only on abstract factories — swap implementations without changing this class.
    /// </summary>
    public class AnimalWorld
    {
        private readonly Factory _herbivoreFactory;
        private readonly Factory _carnivoreFactory;

        public AnimalWorld(Factory herbivoreFactory, Factory carnivoreFactory)
        {
            _herbivoreFactory = herbivoreFactory;
            _carnivoreFactory = carnivoreFactory;
        }

        public void RunFoodChain()
        {
            Animal prey = _herbivoreFactory.CreateAnimal();
            Animal predator = _carnivoreFactory.CreateAnimal();

            Console.WriteLine($"{predator.GetType().Name} ({predator.Type}) eats {prey.GetType().Name} ({prey.Type})");
        }
    }

    public abstract class Animal
    {
        public string? Name { get; set; }
        public AnimalType? Type { get; set; }
    }

    public enum AnimalType
    {
        None,
        Herbivore,
        Carnivore
    }
}
