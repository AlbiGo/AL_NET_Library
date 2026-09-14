using DataManagement.Entities;
using DataManagement.Repositories.Implementations;

namespace Logging.Repositories
{
    public class ExceptionLogRepository : BaseRepository<Log>, IExceptionLogRepository
    {
    }
}