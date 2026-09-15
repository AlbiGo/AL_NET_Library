namespace Dependency.Implementation
{
    /// <summary>
    /// Application service in the DI graph — depends on <see cref="IMathRepo"/> (abstraction).
    /// <para>
    /// Registered in <see cref="AppServices"/>; injected into <see cref="EconomicsController"/>.
    /// Never constructs its own repo — that is the Prefer path this sample teaches.
    /// </para>
    /// </summary>
    public class MathService : IMathService
    {
        private readonly IMathRepo _mathRepo;

        public MathService(IMathRepo mathRepo) => _mathRepo = mathRepo;

        public void Add(Math math)
        {
            Console.WriteLine($"MathService: adding value {math.Value} via IMathRepo");
            _mathRepo.Add(math);
        }
    }
}
