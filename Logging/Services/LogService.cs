using DataManagement.Entities;
using Logging.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Logging.Services
{
    /// <summary>
    /// Boundary logging via repository. Prefer constructor injection over resolving inside the ctor.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly IExceptionLogRepository _exceptionLogRepository;

        public LogService(IExceptionLogRepository exceptionLogRepository)
        {
            _exceptionLogRepository = exceptionLogRepository;
        }

        public Task Log(Log log) => _exceptionLogRepository.Add(log);

        public Task<List<Log>> GetLogs() =>
            _exceptionLogRepository.CustomQuery().ToListAsync();
    }
}
