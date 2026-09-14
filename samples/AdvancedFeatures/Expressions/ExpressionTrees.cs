using System.Linq.Expressions;

namespace AdvancedFeatures.Expressions
{
    /// <summary>
    /// Expression trees describe code as data.
    /// EF Core (and other IQueryable providers) can translate Expression&lt;Func&lt;...&gt;&gt; to SQL.
    /// Calling .Compile() turns the tree into a normal delegate — that runs in memory only.
    /// </summary>
    public static class ExpressionTrees
    {
        /// <summary>
        /// Simplest form: a lambda assigned to Expression&lt;...&gt; is stored as a tree, not executed yet.
        /// </summary>
        public static Expression<Func<int, int, int>> CreateExpressionTreeFromLambdaExpression()
        {
            // Because the variable type is Expression<...>, the compiler builds a tree
            // (nodes for parameters, add, etc.) instead of a runnable method.
            Expression<Func<int, int, int>> sumExpressionTree = (number1, number2) => number1 + number2;
            return sumExpressionTree;
        }

        /// <summary>
        /// Builds one predicate expression from optional filter fields.
        /// Locals (age/email/fullName) are captured into the tree as constant-like values
        /// so providers can parameterize them.
        /// </summary>
        public static Expression<Func<Student, bool>> CreateExpressionTreeFromFilter(StudentFilter filter)
        {
            var age = filter.Age;
            var email = filter.Email;
            var fullName = filter.FullName;

            // Still an Expression — NOT compiled. Safe to pass to IQueryable.Where.
            return p =>
                (age == null || p.Age >= age) &&
                (string.IsNullOrEmpty(email) || p.Email.Contains(email)) &&
                (string.IsNullOrEmpty(fullName) || p.StudentName.Contains(fullName));
        }

        /// <summary>
        /// Applies the filter while keeping the query as IQueryable.
        /// Wrong: query.Where(expr.Compile()) — forces client-side enumeration.
        /// Right: query.Where(expr) — provider may translate to SQL.
        /// </summary>
        public static IQueryable<Student> InlineFilter(this IQueryable<Student> query, StudentFilter filter)
        {
            return query.Where(CreateExpressionTreeFromFilter(filter));
        }

        /// <summary>
        /// Alternative style: add a Where only when that criterion is present.
        /// Often easier for EF to translate than one big expression with many ORs.
        /// </summary>
        public static IQueryable<Student> FilterBy(this IQueryable<Student> query, StudentFilter filter)
        {
            if (filter.Age is int age)
            {
                query = query.Where(p => p.Age >= age);
            }

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
