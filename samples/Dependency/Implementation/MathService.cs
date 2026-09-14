namespace Dependency.Implementation
{
    /// <summary>
    /// Application service — depends on IMathRepo (abstraction), not MathRepo (concrete).
    /// Registered in AppServices; injected into EconomicsController.
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
