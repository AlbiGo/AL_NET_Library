using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Implementation
{
    public class MathRepo : BaseRepo<Math>, IMathRepo 
    {
        public MathRepo(MathDBContext mathDBContext) : base(mathDBContext)
        {
        }
    }
}
