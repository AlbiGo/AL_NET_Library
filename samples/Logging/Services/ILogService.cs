using DataManagement.Entities;

namespace Logging.Services
{
    public interface ILogService
    {
        public Task Log(Log log);

        public Task<List<Log>> GetLogs();
    }
}