using DataManagement.DbContext;
using DataManagement.Entities;
using DataManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataManagement.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository()
        {
            _databaseContext = new DatabaseContext();
            _dbSet = _databaseContext.Set<T>();
        }

        public IQueryable<T> CustomQueryNT()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> CustomQuery()
        {
            return _dbSet;
        }

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

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SoftRemove(T entity)
        {
            MarkSoftDeleted(entity);
            await _databaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Soft-deletes the entity and all related <see cref="BaseEntity"/> navigations that are loaded.
        /// </summary>
        public async Task SoftRemoveRelated(T entity)
        {
            await SoftRemoveRelated(entity, relatedEntityNames: null);
        }

        /// <summary>
        /// Soft-deletes the entity and optionally only the named related navigations.
        /// Pass <paramref name="relatedEntityNames"/> null to include every navigation.
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
            var now = DateTime.UtcNow;
            entity.Updated = now;
            entity.Deleted = now;
        }
    }
}
