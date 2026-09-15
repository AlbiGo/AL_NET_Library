namespace Dependency.Implementation
{
    /// <summary>
    /// Prefer DI consumer — required collaborators arrive through the constructor.
    /// <para>
    /// Asks for <see cref="IMathService"/>, never <c>new MathService(...)</c>.
    /// The container builds Service → Repo → Context. Contrast with
    /// <see cref="EconomicsControllerV2"/> which hard-wires the whole graph.
    /// </para>
    /// </summary>
    public class EconomicsController
    {
        private readonly IMathService _mathService;

        public EconomicsController(IMathService mathService) => _mathService = mathService;

        public void EconomicsCalc(Math math) => _mathService.Add(math);
    }
}
