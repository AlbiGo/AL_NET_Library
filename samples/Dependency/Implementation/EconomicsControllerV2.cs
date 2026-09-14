namespace Dependency.Implementation
{
    /// <summary>
    /// Don't (contrast): manual composition without a container.
    /// Fine for three types; painful as the dependency graph grows.
    /// Prefer <see cref="EconomicsController"/> with DI.
    /// </summary>
    public class EconomicsControllerV2
    {
        private readonly MathService _mathService;

        public EconomicsControllerV2()
        {
            // Hard-wired graph — changing MathRepo's constructor forces edits here too.
            _mathService = new MathService(new MathRepo(new MathDBContext()));
        }

        public void EconomicsCalc(Math math)
        {
            _mathService.Add(math);
        }
    }
}
