using DataManagement.DbContext;
using DataManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedFeatures.CompiledQueries
{
    /// <summary>
    /// Input values for one compiled-filter call (name contains term, created after a date).
    /// </summary>
    public class Filter
    {
        public string? FilterTerm { get; set; }
        public DateTime Created { get; set; }
    }

    /// <summary>
    /// Hot-path query demo using <see cref="EF.CompileQuery"/>.
    /// <para>
    /// Without compiling, EF rebuilds and translates the same LINQ expression on every call.
    /// <c>CompileQuery</c> does that translation <b>once</b>, then reuses the plan with new parameter values.
    /// Pass an already-open <see cref="DatabaseContext"/> — do not open a new context inside the helper.
    /// Use for queries you run often; skip for one-offs (complexity for little gain).
    /// </para>
    /// </summary>
    public static class CompiledQueryEx
    {
        // Compiled once when the type is first used, then reused for every Filter(...) call.
        //
        // Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>>:
        //   1st = open DbContext, 2nd = name term, 3rd = created-after cutoff
        private static readonly Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>> FilterQuery =
            EF.CompileQuery((DatabaseContext context, string term, DateTime created) =>
                context.Entity1s.Where(p =>
                    p.Name.Contains(term) &&
                    p.Created > created));

        /// <summary>
        /// Invokes the cached compiled delegate — same SQL shape, fresh parameter values.
        /// </summary>
        public static IEnumerable<Entity1> Filter(DatabaseContext context, Filter filter)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(filter);

            return FilterQuery(context, filter.FilterTerm ?? string.Empty, filter.Created);
        }
    }
}
