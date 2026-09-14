using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Detach.Repository
{
    /// <summary>
    /// Minimal repository focused on detach behavior.
    /// Pass a shared <see cref="AuditEntry.AuditDbContext"/> so all operations use one store.
    /// </summary>
    public class BaseRepository<T> where T : class
    {
        private readonly AuditEntry.AuditDbContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AuditEntry.AuditDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }

        public IQueryable<T> CustomQuery() => _dbSet;

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public Task SaveChanges() => _databaseContext.SaveChangesAsync();

        public void Detach(T entity)
        {
            if (entity == null)
            {
                return;
            }

            _databaseContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
