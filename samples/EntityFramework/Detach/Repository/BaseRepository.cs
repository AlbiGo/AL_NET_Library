using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Detach.Repository
{
    /// <summary>
    /// Minimal repo to demo EF Change Tracker states — focus is <see cref="Detach"/>.
    /// <para>
    /// After detach, mutating the object in memory does nothing on <c>SaveChanges</c>
    /// until you Attach/Update again. Always inject one shared <c>AuditDbContext</c>
    /// so Add + Detach hit the same in-memory store.
    /// </para>
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

            // Change Tracker drops this instance; SaveChanges will ignore it.
            _databaseContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
