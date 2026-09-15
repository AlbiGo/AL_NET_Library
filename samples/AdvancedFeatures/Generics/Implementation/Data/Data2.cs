namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>
    /// Concrete payload B — different fields, same <see cref="IMainData.Calculate"/> contract.
    /// Interchangeable with <see cref="Data1"/> from <see cref="GenericServices{T}"/>'s point of view.
    /// </summary>
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
