using DataManagement.DbContext;
using DataManagement.Entities;
using DataManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagement.Repositories.Implementations
{
    /// <summary>
    /// Generic repository over BaseEntity.
    /// Soft-delete sets Deleted/Updated instead of removing the row from the database.
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository()
        {
            _databaseContext = new DatabaseContext();
            _dbSet = _databaseContext.Set<T>();
        }

        /// <summary>No-tracking query — faster reads when you will not update the results.</summary>
        public IQueryable<T> CustomQueryNT() => _dbSet.AsNoTracking();

        public IQueryable<T> CustomQuery() => _dbSet;

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _databaseContext.Update(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SaveChanges() => await _databaseContext.SaveChangesAsync();

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity); // hard delete
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SoftRemove(T entity)
        {
            MarkSoftDeleted(entity);
            await _databaseContext.SaveChangesAsync();
        }

        /// <summary>Soft-delete this entity and every related BaseEntity navigation.</summary>
        public Task SoftRemoveRelated(T entity) => SoftRemoveRelated(entity, relatedEntityNames: null);

        /// <summary>
        /// Soft-delete this entity and selected navigations.
        /// Uses EF model metadata (IsCollection) — not string heuristics like "List" in the type name.
        /// relatedEntityNames null ⇒ all navigations; otherwise only matching names (e.g. "Entity3s").
        /// </summary>
        public async Task SoftRemoveRelated(T entity, string[]? relatedEntityNames = null)
        {
            MarkSoftDeleted(entity);

            var entityType = _databaseContext.Model.FindEntityType(typeof(T))
                ?? throw new InvalidOperationException($"Entity type {typeof(T).Name} is not part of the model.");

            foreach (var navigation in entityType.GetNavigations())
            {
                if (relatedEntityNames != null && !relatedEntityNames.Contains(navigation.Name))
                {
                    continue;
                }

                var entry = _dbSet.Entry(entity);

                if (navigation.IsCollection)
                {
                    await entry.Collection(navigation.Name).LoadAsync();
                    var collection = entry.Collection(navigation.Name).CurrentValue;
                    if (collection == null)
                    {
                        continue;
                    }

                    foreach (var related in collection.OfType<BaseEntity>())
                    {
                        MarkSoftDeleted(related);
                    }
                }
                else
                {
                    await entry.Reference(navigation.Name).LoadAsync();
                    var relatedEntry = entry.Reference(navigation.Name).TargetEntry;
                    if (relatedEntry?.Entity is BaseEntity relatedEntity)
                    {
                        MarkSoftDeleted(relatedEntity);
                        relatedEntry.State = EntityState.Modified;
                    }
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Stop tracking this instance. Later property edits are ignored by SaveChanges
        /// unless you Attach/Update again.
        /// </summary>
        public void Detach(T entity)
        {
            if (entity == null)
            {
                return;
            }

            _databaseContext.Entry(entity).State = EntityState.Detached;
        }

        private static void MarkSoftDeleted(BaseEntity entity)
        {
            var now = DateTime.UtcNow; // prefer UTC for stored timestamps
            entity.Updated = now;
            entity.Deleted = now;
        }
    }
}
