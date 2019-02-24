using System.Threading.Tasks;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.DataLayer
{
    public class HistoryRepository : IHistoryRepository
    {
        public Task DriverReadyAtPositionAsync(string driverId, Position position)
        {
            return Task.CompletedTask;
        }
    }
}
