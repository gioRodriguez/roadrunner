using System;
using System.Threading;
using System.Threading.Tasks;
using Roadrunner.DriverClient;
using Roadrunner.Utils;

namespace Roadrunner.DriversSimulator
{
    public class Driver
    {
        public int Id { get; }

        private readonly int _regionWidth;
        private readonly int _regionHeight;
        private readonly TimeSpan _movementTimeSpan;

        private Driver(int id, int regionWidth, int regionHeight, TimeSpan movementTimeSpan)
        {
            Id = id;
            _regionWidth = regionWidth;
            _regionHeight = regionHeight;
            _movementTimeSpan = movementTimeSpan;
        }

        public static Driver Create(int id, int regionWidth, int regionHeight, TimeSpan movementTimeSpan)
        {
            return new Driver(id, regionWidth, regionHeight, movementTimeSpan);
        }

        private Task<TripRequestAnswer> ReceivedTripRequest(TripRequest tripRequest)
        {
            return Task.FromResult(TripRequestAnswer.Accepted());
        }

        public Task StartAsync(CancellationToken ct)
        {
            var driverApiKey = DriversApiKeys.SetupUserApiKey($"driver-{Id}");
            var random = new Random();
            var x = random.Next(0, _regionWidth);
            var y = random.Next(0, _regionHeight);
            return Task.Run(async () =>
            {
                var roadRunnerDriverClient = new RoadRunnerDriverClient.Builder(Id, driverApiKey)
                    .OnReceivedTripRequest(ReceivedTripRequest)
                    .Build();

                

                while (!ct.IsCancellationRequested)
                {
                    await Task.Delay((int)_movementTimeSpan.TotalMilliseconds, ct);                    
                    x += random.Next(-1, 1);
                    y += random.Next(-1, 1);

                    await roadRunnerDriverClient.ReportMovementAsync(x, y);
                    Console.WriteLine($"Hello driver{Id} is at ({x},{y})");                   
                }
            }, ct);
        }


        
    }
}
