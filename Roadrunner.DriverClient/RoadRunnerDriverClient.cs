using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roadrunner.DriverClient
{
    public class RoadRunnerDriverClient
    {
        private readonly int _driverId;
        private readonly string _driverApiKey;
        private readonly Func<TripRequest, Task<TripRequestAnswer>> _receivedTripRequest;

        private RoadRunnerDriverClient(int driverId, string driverApiKey, Func<TripRequest, Task<TripRequestAnswer>> receivedTripRequest)
        {
            _driverId = driverId;
            _driverApiKey = driverApiKey;
            _receivedTripRequest = receivedTripRequest;
        }

        public Task ReportMovementAsync(int x, int y)
        {
            throw new NotImplementedException();
        }

        public class Builder
        {
            private readonly int _driverId;
            private readonly string _driverApiKey;
            private Func<TripRequest, Task<TripRequestAnswer>> _receivedTripRequest;

            public Builder(int driverId, string driverApiKey)
            {
                _driverId = driverId;
                _driverApiKey = driverApiKey;
            }

            public Builder OnReceivedTripRequest(Func<TripRequest, Task<TripRequestAnswer>> receivedTripRequest)
            {
                _receivedTripRequest = receivedTripRequest;
                return this;
            }

            public RoadRunnerDriverClient Build()
            {
                return new RoadRunnerDriverClient(_driverId, _driverApiKey, _receivedTripRequest);
            }
        }        
    }
}
