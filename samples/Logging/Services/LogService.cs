using DataManagement.Entities;
using Logging.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Logging.Services
{
    /// <summary>
    /// Boundary logging service — Prefer ctor injection of the log store.
    /// <para>
    /// The DI container supplies <see cref="IExceptionLogRepository"/>.
    /// Log at process boundaries (Info start, Error on handled failure), then read back via
    /// <see cref="GetLogs"/>. Do not call a service locator inside the constructor when ctor injection is available.
    /// </para>
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
