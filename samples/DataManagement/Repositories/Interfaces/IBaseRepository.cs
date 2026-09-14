using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public IQueryable<T> CustomQueryNT();
        public IQueryable<T> CustomQuery();
        public Task Add(T entity);
        public Task SoftRemove(T entity);
        public Task SoftRemoveRelated(T entity, string[]? relatedEntities = null);
    }
}
