namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>
    /// Shared base so each concrete type only fills in its own <see cref="Calculate"/> logic.
    /// <see cref="GenericServices{T}"/> never branches on the concrete type.
    /// </summary>
    public abstract class MainData : IMainData
    {
        public double MainEconomics { get; set; }

        public abstract void Calculate();
    }
}
