namespace Dependency.Implementation
{
    /// <summary>
    /// Avoid (contrast): manual composition without a container.
    /// <para>
    /// Fine for three types; painful as the dependency graph grows — every ctor change
    /// forces edits here. Prefer <see cref="EconomicsController"/> with DI.
    /// </para>
    /// </summary>
    public class EconomicsControllerV2
    {
        private readonly MathService _mathService;

        public EconomicsControllerV2()
        {
            _mathService = new MathService(new MathRepo(new MathDBContext()));
        }

        public void EconomicsCalc(Math math) => _mathService.Add(math);
    }
}
