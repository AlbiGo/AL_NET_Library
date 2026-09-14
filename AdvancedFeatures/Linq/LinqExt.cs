namespace AdvancedFeatures.Linq
{
    /// <summary>
    /// Tiny custom LINQ operator using <c>yield return</c> (deferred execution).
    /// </summary>
    public static class LinqExt
    {
        public static IEnumerable<int> WherePositive(this IEnumerable<int> source)
        {
            foreach (int element in source)
            {
                if (element > 0)
                {
                    yield return element;
                }
            }
        }
    }
}
