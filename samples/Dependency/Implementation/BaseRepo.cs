using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Implementation
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        public readonly MathDBContext _context;
        protected List<T> EntitySet { get; set; }

        public BaseRepo(MathDBContext mathDBContext) 
        {
            _context = mathDBContext;
            EntitySet = (_context.Entities as List<T>);
        }

        public void Add(T item)
        {
            EntitySet.Add(item);
            Console.WriteLine($"{typeof(T).Name} stored (count={EntitySet.Count})");
        }
    }
}
