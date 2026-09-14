using DataManagement.Entities;
using Logging.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Logging.Services
{
    /// <summary>
    /// Writes / reads Log rows through a repository.
    ///
    /// Constructor injection: the DI container supplies IExceptionLogRepository.
    /// Do not call a service locator inside the constructor when ctor injection is available.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly IExceptionLogRepository _exceptionLogRepository;

        public LogService(IExceptionLogRepository exceptionLogRepository)
        {
            _exceptionLogRepository = exceptionLogRepository;
        }

        public Task Log(Log log) => _exceptionLogRepository.Add(log);

        // ToListAsync comes from EF Core (Microsoft.EntityFrameworkCore), not System.Data.Entity.
        public Task<List<Log>> GetLogs() =>
            _exceptionLogRepository.CustomQuery().ToListAsync();
    }
}
