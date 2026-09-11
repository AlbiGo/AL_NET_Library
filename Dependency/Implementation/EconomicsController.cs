namespace Dependency.Implementation
{
    /// <summary>
    /// Controller resolved from DI with constructor injection (preferred).
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
