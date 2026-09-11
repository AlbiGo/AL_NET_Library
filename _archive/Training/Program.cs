//// See https://aka.ms/new-console-template for more information
using DesignPatterns.Creational.Singleton;
using DesignPatterns.Creational.Factory_Method;
using DesignPatterns.Creational.Abstract_Factory;
using DesignPatterns.Creational.Builder;
using AdvancedFeatures.Generics;
using AdvancedFeatures.Generics.Data;
using AdvancedFeatures.Delegates;
using System.Numerics;

public static class Helper
{
    public static T CheckNull<T>(this T entity, string entityName)
    {
        if (entity == null)
        {
            Console.WriteLine(entityName);
            throw new ArgumentNullException(nameof(entity), $"{entityName} cannot be null");
        }
        return entity;
    }
}

public interface IIdentifiedEntity
{
    public int ID { get; set; }
}

public interface ICreatedEntity
{
    public DateTime Created { get; set;}
}

public interface IBaseEntity : IIdentifiedEntity, ICreatedEntity
{

}

public interface IBaseAnimal : IBaseEntity
{

}

public class BaseAnimal : IBaseAnimal
{
    public BaseAnimal(int iD, DateTime created)
    {
        ID = iD;
        Created = created;
    }

    public int ID { get; set; }
    public DateTime Created { get; set; }

}

public class Wolf : BaseAnimal
{
    public Wolf(int id, DateTime created, string name) : base(id, created)
    {
        Name = name;
    }

    public string Name { get; set;}
}

class Program
{
    static void Main()
    {
        int? at = null;
        //at.CheckNull("At");

        //var wolf = new Wolf(1, DateTime.Now, "Beno");
        //Console.WriteLine(wolf.ID + DateTime.Now.ToString() + wolf.Name);
        //var wolf1 = new Wolf(1,DateTime.Now,"test1");
        //var wolf2 = new Wolf(2, DateTime.Now, "test2");
        //wolf2 = wolf1;
        //Console.WriteLine(wolf2.ID + " " + wolf2.Name);

        var str = "[      [            \"Type of transport\",      \"Cargo ship Bulk carrier\",      \"Cargo ship Container ship\",      \"Cargo ship General cargo\",      \"Cargo ship Large RoPax ferry\",      \"Cargo ship Refrigerated cargo\",      \"Cargo ship RoRo-Ferry\",      \"Cargo ship Vehicle transport\",      \"Freight flights Domestic\",      \"Freight flights Long-haul\",      \"Freight flights Short-haul\",      \"HGV (all diesel) All artics 100% Laden\",      \"HGV (all diesel) All artics 50% Laden\",      \"HGV (all diesel) All artics Average laden\",      \"HGV (all diesel) All HGVs 100% Laden\",      \"HGV (all diesel) All HGVs 50% Laden\",      \"HGV (all diesel) All HGVs Average laden\",      \"HGV (all diesel) All rigids 100% Laden\",      \"HGV (all diesel) All rigids 50% Laden\",      \"HGV (all diesel) All rigids Average laden\",      \"HGV (all diesel) Articulated (>3.5 - 33t) 100% Laden\",      \"HGV (all diesel) Articulated (>3.5 - 33t) 50% Laden\",      \"HGV (all diesel) Articulated (>3.5 - 33t) Average laden\",      \"HGV (all diesel) Articulated (>33t) 100% Laden\",      \"HGV (all diesel) Articulated (>33t) 50% Laden\",      \"HGV (all diesel) Articulated (>33t) Average laden\",      \"HGV (all diesel) Rigid (>17 tonnes) 100% Laden\",      \"HGV (all diesel) Rigid (>17 tonnes) 50% Laden\",      \"HGV (all diesel) Rigid (>17 tonnes) Average laden\",      \"HGV (all diesel) Rigid (>3.5 - 7.5 tonnes) 100% Laden\",      \"HGV (all diesel) Rigid (>3.5 - 7.5 tonnes) 50% Laden\",      \"HGV (all diesel) Rigid (>3.5 - 7.5 tonnes) Average laden\",      \"HGV (all diesel) Rigid (>7.5 tonnes-17 tonnes) 100% Laden\",      \"HGV (all diesel) Rigid (>7.5 tonnes-17 tonnes) 50% Laden\",      \"HGV (all diesel) Rigid (>7.5 tonnes-17 tonnes) Average laden\",      \"HGV refrigerated (all diesel) All artics 100% Laden\",      \"HGV refrigerated (all diesel) All artics 50% Laden\",      \"HGV refrigerated (all diesel) All artics Average laden\",      \"HGV refrigerated (all diesel) All HGVs 100% Laden\",      \"HGV refrigerated (all diesel) All HGVs 50% Laden\",      \"HGV refrigerated (all diesel) All HGVs Average laden\",      \"HGV refrigerated (all diesel) All rigids 100% Laden\",      \"HGV refrigerated (all diesel) All rigids 50% Laden\",      \"HGV refrigerated (all diesel) All rigids Average laden\",      \"HGV refrigerated (all diesel) Articulated (>3.5 - 33t) 100% Laden\",      \"HGV refrigerated (all diesel) Articulated (>3.5 - 33t) 50% Laden\",      \"HGV refrigerated (all diesel) Articulated (>3.5 - 33t) Average laden\",      \"HGV refrigerated (all diesel) Articulated (>33t) 100% Laden\",      \"HGV refrigerated (all diesel) Articulated (>33t) 50% Laden\",      \"HGV refrigerated (all diesel) Articulated (>33t) Average laden\",      \"HGV refrigerated (all diesel) Rigid (>17 tonnes) 100% Laden\",      \"HGV refrigerated (all diesel) Rigid (>17 tonnes) 50% Laden\",      \"HGV refrigerated (all diesel) Rigid (>17 tonnes) Average laden\",      \"HGV refrigerated (all diesel) Rigid (>3.5 - 7.5 tonnes) 100% Laden\",      \"HGV refrigerated (all diesel) Rigid (>3.5 - 7.5 tonnes) 50% Laden\",      \"HGV refrigerated (all diesel) Rigid (>3.5 - 7.5 tonnes) Average laden\",      \"HGV refrigerated (all diesel) Rigid (>7.5 tonnes-17 tonnes) 100% Laden\",      \"HGV refrigerated (all diesel) Rigid (>7.5 tonnes-17 tonnes) 50% Laden\",      \"HGV refrigerated (all diesel) Rigid (>7.5 tonnes-17 tonnes) Average laden\",      \"Rail Freight train \",      \"Sea tanker Crude tanker \",      \"Sea tanker Chemical tanker  \",      \"Sea tanker LNG tanker \",      \"Sea tanker LPG Tanker \",      \"Sea tanker Products tanker  \",      \"Vans Average (up to 3.5 tonnes) Battery Electric Vehicle\",      \"Vans Average (up to 3.5 tonnes) CNG\",      \"Vans Average (up to 3.5 tonnes) Diesel\",      \"Vans Average (up to 3.5 tonnes) LPG\",      \"Vans Average (up to 3.5 tonnes) Petrol\",      \"Vans Average (up to 3.5 tonnes) Unknown\",      \"Vans Class I (up to 1.305 tonnes) Battery Electric Vehicle\",      \"Vans Class I (up to 1.305 tonnes) Diesel\",      \"Vans Class I (up to 1.305 tonnes) Petrol\",      \"Vans Class II (1.305 to 1.74 tonnes) Battery Electric Vehicle\",      \"Vans Class II (1.305 to 1.74 tonnes) Diesel\",      \"Vans Class II (1.305 to 1.74 tonnes) Petrol\",      \"Vans Class III (1.74 to 3.5 tonnes) Battery Electric Vehicle\",      \"Vans Class III (1.74 to 3.5 tonnes) Diesel\",      \"Vans Class III (1.74 to 3.5 tonnes) Petrol\",      \"Custom\"        ]      ]";
        bool b = str.Contains("cargo ship container ship");
        Console.ReadLine();
    }
}


