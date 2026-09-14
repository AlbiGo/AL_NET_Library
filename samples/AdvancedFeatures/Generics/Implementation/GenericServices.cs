using AdvancedFeatures.Generics.Implementation.Data;

namespace AdvancedFeatures.Generics.Implementation
{
    /// <summary>
    /// Generic helper: one API for every T that implements IMainData.
    ///
    /// where T : IMainData  → T must expose Calculate() and MainEconomics.
    /// We call data.Calculate() — the *runtime type* (Data1, Data2, ...) supplies the math.
    /// No switch(data) / cast to Data1. That would defeat the point of generics.
    /// </summary>
    public static class GenericServices<T> where T : IMainData
    {
        public static Task CalculateAsync(T data)
        {
            ArgumentNullException.ThrowIfNull(data);
            data.Calculate(); // polymorphic call
            return Task.CompletedTask;
        }

        public static void Calculate(T data)
        {
            ArgumentNullException.ThrowIfNull(data);
            // Same method body works for Data1, Data2, Data3 because each overrides Calculate().
            data.Calculate();
        }
    }
}
