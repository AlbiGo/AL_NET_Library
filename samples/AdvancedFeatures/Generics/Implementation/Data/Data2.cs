namespace AdvancedFeatures.Generics.Implementation.Data
{
    public class Data2 : MainData
    {
        public double Economic2 { get; set; }

        public override void Calculate()
        {
            Economic2 = 1 + 3;
            MainEconomics = Economic2 * 22;
        }
    }
}
