using System;
using System.Collections.Concurrent;
using System.Linq;
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

        public Task DriverPositionUpdateAsync(string driverId, Position position)
        {
            DriversAtPosition[driverId] = position;
            return Task.CompletedTask;
        }

        public Task<Driver> GetRandomDriverAsync()
        {
            var random = new Random();
            return Task.FromResult(new Driver
            {
                Id = "driver-0"
                //Id = DriversAtPosition.ElementAt(random.Next(0, DriversAtPosition.Count)).Key
            });
        }
    }
}
