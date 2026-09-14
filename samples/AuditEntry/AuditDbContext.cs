using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuditEntry
{
    /// <summary>
    /// In-memory EF context that writes an audit trail on every SaveChangesAsync.
    ///
    /// Flow:
    /// 1. ChangeTracker lists Added/Modified/Deleted entities (skip audit tables themselves).
    /// 2. For each, create AuditEntry + AuditEntryProperty (old/new values).
    /// 3. Then call base.SaveChangesAsync so business rows AND audit rows persist together.
    ///
    /// Use the same databaseName string when multiple components must share one in-memory DB.
    /// </summary>
    public class AuditDbContext : DbContext
    {
        private readonly string _databaseName;

        public AuditDbContext()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>Share <paramref name="databaseName"/> across repositories so they hit the same store.</summary>
        public AuditDbContext(string databaseName)
        {
            _databaseName = databaseName;
        }

        public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();
        public DbSet<AuditEntryProperty> AuditEntryProperties => Set<AuditEntryProperty>();
        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(_databaseName);
            }
        }

        // Must override the real EF method — a custom SaveChangesAsync(string) alone would hide base and skip auditing.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => SaveChangesWithAuditAsync("system", cancellationToken);

        public Task<int> SaveChangesWithAuditAsync(string userId, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges(userId);
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries()
                .Where(entry => entry.Entity is not AuditEntry and not AuditEntryProperty
                                && entry.State is not (EntityState.Detached or EntityState.Unchanged))
                .ToList();

            foreach (var entry in entries)
            {
                var auditEntry = new AuditEntry
                {
                    EntityName = entry.Entity.GetType().Name,
                    UserId = int.TryParse(userId, out var id) ? id : 0,
                    Type = entry.State.ToString(),
                    EntityID = GetEntityPrimaryKey(entry),
                    Created = DateTime.UtcNow,
                    AuditEntryProperties = new List<AuditEntryProperty>()
                };

                foreach (var property in entry.Properties)
                {
                    var auditProperty = CreateAuditEntryProperty(entry, property);
                    if (auditProperty != null)
                    {
                        auditEntry.AuditEntryProperties.Add(auditProperty);
                    }
                }

                AuditEntries.Add(auditEntry);
            }
        }

        private static int GetEntityPrimaryKey(EntityEntry entry)
        {
            var primaryKeyProperty = entry.Properties
                .FirstOrDefault(p => p.Metadata.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

            if (primaryKeyProperty?.CurrentValue == null)
            {
                return 0;
            }

            return Convert.ToInt32(primaryKeyProperty.CurrentValue);
        }

        private static AuditEntryProperty? CreateAuditEntryProperty(EntityEntry entry, PropertyEntry property)
        {
            var auditEntryProperty = new AuditEntryProperty
            {
                PropertyName = property.Metadata.Name,
                Modified = property.IsModified ? "Yes" : "No"
            };

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntryProperty.PropertyNewValue = property.CurrentValue?.ToString();
                    break;
                case EntityState.Deleted:
                    auditEntryProperty.PropertyOldValue = property.OriginalValue?.ToString();
                    break;
                case EntityState.Modified when property.IsModified:
                    auditEntryProperty.PropertyOldValue = property.OriginalValue?.ToString();
                    auditEntryProperty.PropertyNewValue = property.CurrentValue?.ToString();
                    break;
                default:
                    return null;
            }

            return auditEntryProperty;
        }
    }
}
