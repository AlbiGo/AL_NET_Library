namespace DesignPatterns.Creational.Abstract_Factory
{
    /// <summary>
    /// Abstract factory — creates one product in a family without exposing concrete types.
    /// Clients depend on this, not on <see cref="Wolf"/> / <see cref="Bison"/> constructors.
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
    /// Client of Abstract Factory — depends only on abstract <see cref="Factory"/> instances.
    /// <para>
    /// Never calls <c>new Wolf()</c> / <c>new Bison()</c>. Swap
    /// <see cref="HerbivoreFactory"/> / <see cref="CarnivoreFactory"/> without editing this class.
    /// One line of output makes the “family of products” idea obvious.
    /// </para>
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
