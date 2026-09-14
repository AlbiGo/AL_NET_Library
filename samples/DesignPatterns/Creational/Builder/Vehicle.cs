namespace DesignPatterns.Creational.Builder
{
    /// <summary>Product assembled step by step.</summary>
    public class Vehicle
    {
        internal List<string> Parts { get; } = new();

        public void PrintParts()
        {
            foreach (var part in Parts)
            {
                Console.WriteLine($"  - {part}");
            }
        }
    }

    /// <summary>Builder steps shared by every vehicle type.</summary>
    public abstract class VehicleBuilder
    {
        public abstract void BuildDoors();
        public abstract void BuildWheels();
        public abstract void BuildEngine();
        public abstract void BuildFrame();
        public abstract Vehicle GetVehicle();
    }

    public class CarBuilder : VehicleBuilder
    {
        private readonly Vehicle _car = new();

        public override void BuildDoors() => _car.Parts.Add("4 Doors");
        public override void BuildWheels() => _car.Parts.Add("4 Wheels");
        public override void BuildEngine() => _car.Parts.Add("2.0 TDI Engine");
        public override void BuildFrame() => _car.Parts.Add("Car Frame");
        public override Vehicle GetVehicle() => _car;
    }

    /// <summary>Second concrete builder — same Shop steps, different product.</summary>
    public class MotorcycleBuilder : VehicleBuilder
    {
        private readonly Vehicle _bike = new();

        public override void BuildDoors() => _bike.Parts.Add("0 Doors");
        public override void BuildWheels() => _bike.Parts.Add("2 Wheels");
        public override void BuildEngine() => _bike.Parts.Add("600cc Engine");
        public override void BuildFrame() => _bike.Parts.Add("Bike Frame");
        public override Vehicle GetVehicle() => _bike;
    }

    /// <summary>
    /// Director — fixed construction order.
    /// Pass CarBuilder or MotorcycleBuilder to get different results from the same steps.
    /// </summary>
    public class Shop
    {
        public Vehicle Construct(VehicleBuilder builder)
        {
            builder.BuildFrame();
            builder.BuildEngine();
            builder.BuildWheels();
            builder.BuildDoors();
            return builder.GetVehicle();
        }
    }
}
