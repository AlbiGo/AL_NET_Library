namespace AdvancedFeatures.Linq
{
    /// <summary>
    /// Custom LINQ-style operator demo — deferred execution via <c>yield return</c>.
    /// <para>
    /// Nothing runs until someone <c>foreach</c>es / <c>ToList</c>s the result.
    /// Same laziness idea as LINQ’s <c>Where</c>, written by hand so the pause/resume is visible.
    /// </para>
    /// </summary>
    public static class LinqExt
    {
        /// <summary>
        /// Keeps only values &gt; 0. Each <c>yield return</c> hands one element to the caller
        /// and pauses until the next <c>MoveNext</c>.
        /// </summary>
        public static IEnumerable<int> WherePositive(this IEnumerable<int> source)
        {
            foreach (int element in source)
            {
                if (element > 0)
                    yield return element;
            }
        }
    }
}
