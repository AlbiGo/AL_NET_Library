namespace AdvancedFeatures.Generics.Implementation.Data
{
    public abstract class MainData : IMainData
    {
        public double MainEconomics { get; set; }

        public abstract void Calculate();
    }
}
