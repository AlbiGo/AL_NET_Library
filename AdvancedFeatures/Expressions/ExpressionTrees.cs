using System.Linq.Expressions;

namespace AdvancedFeatures.Expressions
{
    public static class ExpressionTrees
    {
        public static Expression<Func<int, int, int>> CreateExpressionTreeFromLambdaExpression()
        {
            Expression<Func<int, int, int>> sumExpressionTree = (number1, number2) => number1 + number2;
            return sumExpressionTree;
        }

        /// <summary>
        /// Builds a filter expression suitable for IQueryable providers (e.g. EF Core).
        /// Prefer composing Where clauses when optional filters should stay SQL-translatable.
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
        /// Keeps the expression as an Expression tree so EF can translate it to SQL.
        /// Do not call <c>.Compile()</c> on IQueryable — that forces client-side evaluation.
        /// </summary>
        public static IQueryable<Student> InlineFilter(this IQueryable<Student> query, StudentFilter filter)
        {
            return query.Where(CreateExpressionTreeFromFilter(filter));
        }

        /// <summary>
        /// Alternative style: apply each optional criterion as its own Where (often clearer for EF).
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
