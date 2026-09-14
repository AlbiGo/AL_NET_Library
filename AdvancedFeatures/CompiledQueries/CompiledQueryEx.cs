using DataManagement.DbContext;
using DataManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedFeatures.CompiledQueries
{
    public class Filter
    {
        public string? FilterTerm { get; set; }
        public DateTime Created { get; set; }
    }

    /// <summary>
    /// EF.CompileQuery caches a query shape for repeated execution with different parameters.
    /// Prefer this over rebuilding the same expression tree on every call in hot paths.
    /// </summary>
    public static class CompiledQueryEx
    {
        private static readonly Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>> FilterQuery =
            EF.CompileQuery((DatabaseContext context, string term, DateTime created) =>
                context.Entity1s.Where(p =>
                    p.Name.Contains(term) &&
                    p.Created > created));

        public static IEnumerable<Entity1> Filter(DatabaseContext context, Filter filter)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(filter);
            return FilterQuery(context, filter.FilterTerm ?? string.Empty, filter.Created);
        }
    }
}
