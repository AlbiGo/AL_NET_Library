namespace AdvancedFeatures.Generics.Implementation.Data
{
    public class Data1 : MainData
    {
        public double Economics { get; set; }

        // Called via GenericServices&lt;Data1&gt;.Calculate — no cast required in the helper.
        public override void Calculate()
        {
            Economics = 1 + 3;
            MainEconomics = Economics * 22;
        }
    }
}
