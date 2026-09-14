using DataManagement.DbContext;
using DataManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedFeatures.CompiledQueries
{
    /// <summary>
    /// Input values for the compiled filter (name contains term, created after a date).
    /// </summary>
    public class Filter
    {
        public string? FilterTerm { get; set; }
        public DateTime Created { get; set; }
    }

    /// <summary>
    /// Demonstrates <see cref="EF.CompileQuery"/>.
    /// Without compiling, EF rebuilds and translates the same LINQ expression on every call.
    /// CompileQuery does that translation once, then reuses the plan with new parameter values.
    /// </summary>
    public static class CompiledQueryEx
    {
        // Compiled once when the type is first used, then reused for every Filter(...) call.
        //
        // Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>> means:
        //   1st arg  = the open DbContext to query against
        //   2nd arg  = name search term  (bound to SQL parameter, not concatenated)
        //   3rd arg  = "created after" cutoff
        //   result   = matching Entity1 rows
        //
        // EF.CompileQuery(...):
        //   - Takes an expression tree describing the query
        //   - Translates it to a reusable delegate
        //   - Later invocations only supply fresh context + parameter values
        private static readonly Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>> FilterQuery =
            EF.CompileQuery((DatabaseContext context, string term, DateTime created) =>
                context.Entity1s.Where(p =>
                    p.Name.Contains(term) &&
                    p.Created > created));

        /// <summary>
        /// Runs the compiled query. Pass an already-open <paramref name="context"/>;
        /// do not create a new context inside the query helper.
        /// </summary>
        public static IEnumerable<Entity1> Filter(DatabaseContext context, Filter filter)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(filter);

            // Invoke the cached delegate — same SQL shape, new parameter values each time.
            return FilterQuery(context, filter.FilterTerm ?? string.Empty, filter.Created);
        }
    }
}
