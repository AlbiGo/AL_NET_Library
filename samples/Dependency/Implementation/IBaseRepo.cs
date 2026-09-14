using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Implementation
{
    public interface IBaseRepo<T> where T : class
    {
        public void Add(T item);
    }
}
