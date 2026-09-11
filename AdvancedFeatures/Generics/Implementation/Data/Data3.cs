namespace AdvancedFeatures.Generics.Implementation.Data
{
    public class Data3 : MainData
    {
        public double Economics3 { get; set; }

        public override void Calculate()
        {
            Economics3 = 1 + 314;
            MainEconomics = Economics3 * 22;
        }
    }
}
