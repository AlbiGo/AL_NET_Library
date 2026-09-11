namespace Dependency.Implementation
{
    /// <summary>
    /// Manual composition without a container (harder to maintain as the graph grows).
    /// Prefer <see cref="EconomicsController"/> with DI.
    /// </summary>
    public class EconomicsControllerV2
    {
        private readonly MathService _mathService;

        public EconomicsControllerV2()
        {
            _mathService = new MathService(new MathRepo(new MathDBContext()));
        }

        public void EconomicsCalc(Math math)
        {
            _mathService.Add(math);
        }
    }
}
