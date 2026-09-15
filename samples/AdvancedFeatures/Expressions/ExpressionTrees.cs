using System.Linq.Expressions;

namespace AdvancedFeatures.Expressions
{
    /// <summary>
    /// Expression-tree demo — code as data, not as a running method.
    /// <para>
    /// EF Core (and other <c>IQueryable</c> providers) can translate
    /// <c>Expression&lt;Func&lt;…&gt;&gt;</c> to SQL. Calling <c>.Compile()</c> turns the tree into a
    /// normal delegate that runs <b>in memory only</b> — that is the mistake this sample exists to show.
    /// Prefer <see cref="InlineFilter"/> / pass the expression to <c>Where</c>; avoid
    /// <c>query.Where(expr.Compile())</c> on EF queries.
    /// </para>
    /// </summary>
    public static class ExpressionTrees
    {
        /// <summary>
        /// Simplest form: a lambda assigned to <c>Expression&lt;…&gt;</c> is stored as a tree, not executed yet.
        /// </summary>
        public static Expression<Func<int, int, int>> CreateExpressionTreeFromLambdaExpression()
        {
            Expression<Func<int, int, int>> sumExpressionTree = (number1, number2) => number1 + number2;
            return sumExpressionTree;
        }

        /// <summary>
        /// Builds one predicate expression from optional filter fields.
        /// Locals are captured into the tree so providers can parameterize them.
        /// Still an Expression — NOT compiled. Safe for <c>IQueryable.Where</c>.
        /// </summary>
        public static Expression<Func<Student, bool>> CreateExpressionTreeFromFilter(StudentFilter filter)
        {
            var age = filter.Age;
            var email = filter.Email;
            var fullName = filter.FullName;

            return p =>
                (age == null || p.Age >= age) &&
                (string.IsNullOrEmpty(email) || p.Email.Contains(email)) &&
                (string.IsNullOrEmpty(fullName) || p.StudentName.Contains(fullName));
        }

        /// <summary>
        /// Applies the filter while keeping the query as <c>IQueryable</c>.
        /// Wrong: <c>query.Where(expr.Compile())</c> — forces client-side enumeration.
        /// Right: <c>query.Where(expr)</c> — provider may translate to SQL.
        /// </summary>
        public static IQueryable<Student> InlineFilter(this IQueryable<Student> query, StudentFilter filter)
            => query.Where(CreateExpressionTreeFromFilter(filter));

        /// <summary>
        /// Alternative style: add a <c>Where</c> only when that criterion is present.
        /// Often easier for EF to translate than one big expression with many ORs.
        /// </summary>
        public static IQueryable<Student> FilterBy(this IQueryable<Student> query, StudentFilter filter)
        {
            if (filter.Age is int age)
                query = query.Where(p => p.Age >= age);

            if (!string.IsNullOrEmpty(filter.Email))
            {
                var email = filter.Email;
                query = query.Where(p => p.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                var fullName = filter.FullName;
                query = query.Where(p => p.StudentName.Contains(fullName));
            }

            return query;
        }
    }
}
