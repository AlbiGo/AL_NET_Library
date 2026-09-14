namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>Shared base so each concrete type only fills in its own Calculate logic.</summary>
    public abstract class MainData : IMainData
    {
        public double MainEconomics { get; set; }

        public abstract void Calculate();
    }
}
