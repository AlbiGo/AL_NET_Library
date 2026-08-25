namespace DesignPatterns.Creational.Abstract_Factory
{
    public abstract class Factory
    {
        public abstract Animal CreateAnimal();
    }

    public class CarnivoreFactory : Factory
    {
        public override Animal CreateAnimal()
        {
            return new Wolf();
        }
    }

    public class HerbivoreFactory : Factory
    {
        public override Animal CreateAnimal()
        {
            return new Bison();
        }
    }

    public class Wolf : Animal
    {
    }

    public class Bison : Animal
    {
    }

    public class AnimalWorld
    {
        private readonly HerbivoreFactory _herbivoreFactory;
        private readonly CarnivoreFactory _carnivoreFactory;

        public AnimalWorld(HerbivoreFactory herbivoreFactory, CarnivoreFactory carnivoreFactory)
        {
            _herbivoreFactory = herbivoreFactory;
            _carnivoreFactory = carnivoreFactory;
        }

        public void RunFoodChain()
        {
            var bison = _herbivoreFactory.CreateAnimal();
            var wolf = _carnivoreFactory.CreateAnimal();

            Console.WriteLine(wolf.GetType().Name + " eats " + bison.GetType().Name);
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
