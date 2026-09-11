using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServices
{
    public class CarServices<T> where T : IEntity, ICarServices
    {
        private readonly T _car;
        public CarServices(T car)
        {
            _car = car;
        }

        public void OilChange()
        {
            Console.WriteLine($"{_car.Name} oil changed");
        }

        public void TireChange()
        {
            Console.WriteLine($"{_car.Name} tires changed");
        }

        public void EngineService()
        {
            Console.WriteLine($"{_car.Name} engine serviced");
        }
    }

    public class TEntity : IEntity
    {
        public string Name { get; set; }
    }

    public interface IEntity
    {
        public string Name { get; set; }
    }

    public interface ICarServices
    {
        public void OilChange();

        public void TireChange();

        public void EngineService();
    }
}
