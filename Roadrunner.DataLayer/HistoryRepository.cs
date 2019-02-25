using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.DataLayer
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ILogger<HistoryRepository> _logger;

        public HistoryRepository(ILogger<HistoryRepository> logger)
        {
            _logger = logger;
        }

        public Task DriverReadyAtPositionAsync(string driverId, Position position)
        {
            //_logger.LogInformation($"{driverId} : {position.X},{position.Y}");
            return Task.CompletedTask;
        }

        public Task DriverPositionUpdateAsync(string driverId, Position position)
        {
            //_logger.LogInformation($"{driverId} : {position.X},{position.Y}");
            return Task.CompletedTask;
        }
    }
}
