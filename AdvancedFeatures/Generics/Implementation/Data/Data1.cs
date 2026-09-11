namespace AdvancedFeatures.Generics.Implementation.Data
{
    public class Data1 : MainData
    {
        public double Economics { get; set; }

        public override void Calculate()
        {
            Economics = 1 + 3;
            MainEconomics = Economics * 22;
        }
    }
}
