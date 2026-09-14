namespace AdvancedFeatures.Linq
{
    /// <summary>
    /// Custom LINQ-style operator.
    /// yield return = deferred execution: nothing runs until someone foreach / ToList()s the result.
    /// </summary>
    public static class LinqExt
    {
        /// <summary>
        /// Keeps only values &gt; 0. Same idea as Where(x =&gt; x &gt; 0), written by hand for teaching.
        /// </summary>
        public static IEnumerable<int> WherePositive(this IEnumerable<int> source)
        {
            foreach (int element in source)
            {
                if (element > 0)
                {
                    // Pause here and hand this element to the caller; resume on next MoveNext.
                    yield return element;
                }
            }
        }
    }
}
