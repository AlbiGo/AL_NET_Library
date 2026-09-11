using AdvancedFeatures.Generics.Implementation.Data;

namespace AdvancedFeatures.Generics.Implementation
{
    /// <summary>
    /// Generic helper constrained to <see cref="IMainData"/>.
    /// Type-specific logic lives on each implementation via <see cref="IMainData.Calculate"/> —
    /// no switch/cast on T.
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
            data.Calculate();
        }
    }
}
