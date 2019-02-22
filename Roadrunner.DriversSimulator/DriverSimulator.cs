using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roadrunner.DriversSimulator
{
    public class DriverSimulator
    {
        public int NumberOfDriversToSimulate { get; private set; }
        public int RegionWidth { get; private set; }
        public int RegionHeight { get; private set; }
        public TimeSpan MovementTimeSpan { get; private set; }

        private DriverSimulator()
        {}

        public Task StartSimulationAsync(CancellationToken ct)
        {
            var drivers = new List<Driver>();
            for (var i = 0; i < NumberOfDriversToSimulate; i++)
            {
                drivers.Add(Driver.Create(i, RegionWidth, RegionHeight, MovementTimeSpan));
            }

            var tasks = drivers.Select(driver => driver.StartAsync(ct));

            return Task.WhenAll(tasks);
        }

        public class Builder
        {
            private int _numberOfDriversToSimulate = 5;
            private int _regionWidth = 100;
            private int _regionHeight = 100;
            private TimeSpan _movementTimeSpan = TimeSpan.FromSeconds(1);

            public Builder NumberOfDrivers(int numberOfDriversToSimulate)
            {
                _numberOfDriversToSimulate = numberOfDriversToSimulate;
                return this;
            }

            public Builder RegionSize(int width, int height)
            {
                _regionWidth = width;
                _regionHeight = height;
                return this;
            }

            public Builder MovementTimeSpan(TimeSpan movementTimeSpan)
            {
                _movementTimeSpan = movementTimeSpan;
                return this;
            }

            public DriverSimulator Build()
            {
                // TODO: validation rules
                if (_numberOfDriversToSimulate <= 0)
                {
                    _numberOfDriversToSimulate = 5;
                }

                return new DriverSimulator
                {
                    NumberOfDriversToSimulate = _numberOfDriversToSimulate,
                    RegionWidth = _regionWidth,
                    RegionHeight = _regionHeight,
                    MovementTimeSpan = _movementTimeSpan
                };
            }
        }        
    }
}
