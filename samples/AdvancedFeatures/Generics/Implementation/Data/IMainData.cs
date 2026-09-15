namespace AdvancedFeatures.Generics.Implementation.Data
{
    /// <summary>
    /// Contract for economic payloads. <see cref="GenericServices{T}"/> only needs this surface.
    /// <para>
    /// Put behavior on the type via <see cref="Calculate"/> — that is what keeps the generic helper tiny
    /// and free of <c>switch</c>/<c>as</c> on <c>T</c>.
    /// </para>
    /// </summary>
    public interface IMainData
    {
        double MainEconomics { get; set; }

        /// <summary>Type-specific calculation — implemented on Data1/Data2/Data3.</summary>
        void Calculate();
    }
}
