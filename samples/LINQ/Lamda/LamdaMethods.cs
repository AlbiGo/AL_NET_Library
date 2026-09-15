using DataManagement.DbContext;
using DataManagement.Entities;

namespace LINQ.Lamda
{
    /// <summary>
    /// EF-backed LINQ helpers — Prefer rules for filter, pagination, and joins.
    /// <para>
    /// Keep returning <c>IQueryable</c> so callers can compose and EF can translate to SQL
    /// (do not <c>ToList</c> too early). Pagination uses 1-based pages:
    /// <c>Skip((page - 1) * size)</c> — never <c>Skip(page)</c>. Joins project both sides into a DTO.
    /// </para>
    /// </summary>
    public class LamdaMethods
    {
        private readonly DatabaseContext _dbContext;

        public LamdaMethods()
        {
            _dbContext = new DatabaseContext();
        }

        public IQueryable<Entity1> GetAll() => _dbContext.Entity1s.AsQueryable();

        /// <summary>
        /// Pagination: page is 1-based.
        /// Skip((page - 1) * size) — NOT Skip(page), which would skip the wrong number of rows.
        /// </summary>
        public IQueryable<Entity1> GetAllPagination(int page = 1, int size = 10)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;

            return _dbContext.Entity1s
                .Skip((page - 1) * size)
                .Take(size);
        }

        public IQueryable<Entity1> GetWhere(EntityFilter entityFilter)
        {
            var query = _dbContext.Entity1s.AsQueryable();

            // Only add a Where when the filter has a value (keeps SQL simpler).
            if (!string.IsNullOrWhiteSpace(entityFilter.Name))
            {
                query = query.Where(p => p.Name.Contains(entityFilter.Name));
            }

            return ApplyPagination(query, entityFilter);
        }

        /// <summary>
        /// Join projects BOTH sides into a DTO.
        /// Wrong pattern: (e1, e2) => e1 then use e1.Entity2.Name without Include
        /// (navigation may be null / extra query).
        /// </summary>
        public IQueryable<Entity1WithRelationshipDTO> GetJoin()
        {
            return _dbContext.Entity1s
                .Join(
                    _dbContext.Entity2s,
                    entity1 => entity1.Entity2ID,
                    entity2 => entity2.Id,
                    (entity1, entity2) => new Entity1WithRelationshipDTO
                    {
                        ID = entity1.Id,
                        Entity1Name = entity1.Name,
                        Entity2Name = entity2.Name // use joined entity2, not navigation
                    });
        }

        public IQueryable<Entity1WithRelationshipDTO> GetJoinWithFilter(EntityFilter entityFilter)
        {
            var query = _dbContext.Entity1s.AsQueryable();

            if (!string.IsNullOrWhiteSpace(entityFilter.Name))
            {
                query = query.Where(p => p.Name.Contains(entityFilter.Name));
            }

            var joined = query.Join(
                _dbContext.Entity2s,
                entity1 => entity1.Entity2ID,
                entity2 => entity2.Id,
                (entity1, entity2) => new Entity1WithRelationshipDTO
                {
                    ID = entity1.Id,
                    Entity1Name = entity1.Name,
                    Entity2Name = entity2.Name
                });

            return ApplyPagination(joined, entityFilter);
        }

        private static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, EntityFilter filter)
        {
            var page = filter.Page < 1 ? 1 : filter.Page;
            var size = filter.Size < 1 ? 10 : filter.Size;
            return query.Skip((page - 1) * size).Take(size);
        }
    }

    /// <summary>Flat result of a join — no tracked entities, just data for the UI/API.</summary>
    public class Entity1WithRelationshipDTO
    {
        public int ID { get; set; }
        public string? Entity1Name { get; set; }
        public string? Entity2Name { get; set; }
    }

    public class EntityFilter
    {
        public string? Name { get; set; }
        /// <summary>1-based page number.</summary>
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
