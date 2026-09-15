using AdvancedFeatures.Generics.Implementation.Data;

namespace AdvancedFeatures.Generics.Implementation
{
    /// <summary>
    /// Generic helper for the Prefer path — one API for every <typeparamref name="T"/> that implements <see cref="IMainData"/>.
    /// <para>
    /// <c>where T : IMainData</c> guarantees <c>Calculate()</c> and <c>MainEconomics</c>.
    /// We call <c>data.Calculate()</c>; the <b>runtime type</b> (<see cref="Data.Data1"/>, <see cref="Data.Data2"/>, …)
    /// supplies the math. No <c>switch(data)</c> / cast to <c>Data1</c> — that would defeat generics:
    /// every new type would force edits here.
    /// </para>
    /// </summary>
    public static class GenericServices<T> where T : IMainData
    {
        public static Task CalculateAsync(T data)
        {
            ArgumentNullException.ThrowIfNull(data);
            data.Calculate();
            return Task.CompletedTask;
        }

        public static void Calculate(T data)
        {
            ArgumentNullException.ThrowIfNull(data);
            // Same body for Data1/Data2/Data3 — each overrides Calculate().
            data.Calculate();
        }
    }
}
