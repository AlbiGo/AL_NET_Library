using DataManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataManagement.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext()
        {
            this.InitiateDatabase();
        }

        public DbSet<Entity1> Entity1s { get; set; }
        public DbSet<Entity2> Entity2s { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppContext.BaseDirectory, "DataManagement.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set database default Filtering
            const string FilteredDelete = "Deleted IS NULL";
            modelBuilder.Entity<Entity1>().HasIndex(p => new { p.Deleted, p.Id }).IsUnique(true).HasFilter(FilteredDelete);
            modelBuilder.Entity<Entity2>().HasIndex(p => new { p.Deleted, p.Id }).IsUnique(true).HasFilter(FilteredDelete);
        }

        private void InitiateDatabase()
        {
            var dbScript = this.Database.GenerateCreateScript();

            //Fix script to adapt to SQLite
            dbScript = dbScript.Replace("smallint", "integer");
            dbScript = dbScript.Replace("varchar(50)", "varchar[50]");
            dbScript = dbScript.Replace("AUTOINCREMENT", "");

            //Flush old DB if there
            this.Database.EnsureDeleted();

            //Create DB
            this.Database.ExecuteSqlRaw(dbScript);
        }
    }
}