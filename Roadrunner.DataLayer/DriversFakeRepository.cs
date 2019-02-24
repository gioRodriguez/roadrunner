using System.Collections.Concurrent;
using System.Threading.Tasks;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.DataLayer
{
    public class DriversFakeRepository : IDriversRepository
    {
        private static readonly ConcurrentDictionary<string, Position> DriversAtPosition = new ConcurrentDictionary<string, Position>();

        public Task DriverReadyAtPositionAsync(string driverId, Position position)
        {
            DriversAtPosition[driverId] = position;
            return Task.CompletedTask;
        }
    }
}
