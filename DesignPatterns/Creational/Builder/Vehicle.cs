namespace DesignPatterns.Creational.Builder
{
    public class Vehicle
    {
        internal List<string> vehicleParts = new List<string>();

        public void PrintParts()
        {
            foreach (var part in vehicleParts)
            {
                Console.WriteLine(part);
            }
        }
    }

    public class VehiclePart
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }

    public abstract class VehicleBuilder
    {
        public abstract void BuildDoors();
        public abstract void BuildWheels();
        public abstract void BuildEngine();
        public abstract void BuildFrame();
        public abstract Vehicle GetVehicle();
    }

    public class Car : Vehicle
    {
    }

    public class CarBuilder : VehicleBuilder
    {
        private Car car = new Car();

        public override void BuildDoors()
        {
            car.vehicleParts.Add("4 Doors");
        }

        public override void BuildWheels()
        {
            car.vehicleParts.Add("4 Wheels");
        }

        public override void BuildEngine()
        {
            car.vehicleParts.Add("2.0 TDI Engine");
        }

        public override void BuildFrame()
        {
            car.vehicleParts.Add("Car Frame");
        }

        public override Car GetVehicle()
        {
            return car;
        }
    }

    public class Shop
    {
        public void Construct(VehicleBuilder builder)
        {
            builder.BuildDoors();
            builder.BuildWheels();
            builder.BuildEngine();
            builder.BuildFrame();
        }
    }
}
