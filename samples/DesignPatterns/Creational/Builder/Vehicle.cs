namespace DesignPatterns.Creational.Builder
{
    /// <summary>Product assembled step by step — a list of part names for the demo.</summary>
    public class Vehicle
    {
        internal List<string> Parts { get; } = new();

        public void PrintParts()
        {
            foreach (var part in Parts)
                Console.WriteLine($"  - {part}");
        }
    }

    /// <summary>Builder steps shared by every vehicle type (frame, engine, wheels, doors).</summary>
    public abstract class VehicleBuilder
    {
        public abstract void BuildDoors();
        public abstract void BuildWheels();
        public abstract void BuildEngine();
        public abstract void BuildFrame();
        public abstract Vehicle GetVehicle();
    }

    /// <summary>Concrete builder — car parts. Same <see cref="Shop"/> steps as <see cref="MotorcycleBuilder"/>.</summary>
    public class CarBuilder : VehicleBuilder
    {
        private readonly Vehicle _car = new();

        public override void BuildDoors() => _car.Parts.Add("4 Doors");
        public override void BuildWheels() => _car.Parts.Add("4 Wheels");
        public override void BuildEngine() => _car.Parts.Add("2.0 TDI Engine");
        public override void BuildFrame() => _car.Parts.Add("Car Frame");
        public override Vehicle GetVehicle() => _car;
    }

    /// <summary>Second concrete builder — bike parts from the same director recipe.</summary>
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
    /// Director — fixed construction order; not the part list itself.
    /// <para>
    /// Pass <see cref="CarBuilder"/> or <see cref="MotorcycleBuilder"/> to get different products
    /// from the same steps. Same split as garage vs <c>CarServices</c>: orchestration vs work.
    /// </para>
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
