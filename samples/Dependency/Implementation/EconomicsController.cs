namespace Dependency.Implementation
{
    /// <summary>
    /// Do: required dependencies arrive through the constructor.
    /// The container creates MathService (+ MathRepo) — this class never calls new on them.
    /// </summary>
    public class EconomicsController
    {
        private readonly IMathService _mathService;

        public EconomicsController(IMathService mathService)
        {
            _mathService = mathService;
        }

        public void EconomicsCalc(Math math)
        {
            _mathService.Add(math);
        }
    }
}
