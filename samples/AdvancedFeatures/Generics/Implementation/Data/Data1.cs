namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>
    /// Concrete payload A — supplies its own <see cref="Calculate"/> math.
    /// <para>
    /// Called via <c>GenericServices&lt;Data1&gt;.Calculate(data1)</c>.
    /// The helper never casts to <c>Data1</c>; polymorphism does the work.
    /// </para>
    /// </summary>
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
