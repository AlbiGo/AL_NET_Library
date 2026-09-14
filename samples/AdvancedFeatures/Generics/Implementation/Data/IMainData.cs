namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>
    /// Contract for economic payloads. GenericServices&lt;T&gt; only needs this surface.
    /// </summary>
    public interface IMainData
    {
        double MainEconomics { get; set; }

        /// <summary>Type-specific calculation — implemented on Data1/Data2/Data3.</summary>
        void Calculate();
    }
}
