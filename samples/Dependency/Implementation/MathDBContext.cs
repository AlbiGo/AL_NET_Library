using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Implementation
{
    public class MathDBContext
    {
        public List<Math> Entities = new List<Math>();
        public MathDBContext() { }

        public void AddEntity(Math entity)
        {
            Entities.Add(entity);
            Console.WriteLine("Entity Added");
        }
        public void RemoveEntity(Math entity)
        {
            Entities.Remove(entity);
        }
    }
}
