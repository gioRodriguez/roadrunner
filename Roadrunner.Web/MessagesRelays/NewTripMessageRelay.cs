using System;
using Microsoft.AspNetCore.SignalR;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Web.Hubs;

namespace Roadrunner.Web.MessagesRelays
{
    public class NewTripMessageRelay
    {
        private readonly IHubContext<DriverHub> _driversHubContext;
        private readonly IDisposable _sub;

        public NewTripMessageRelay(ITripsProcessor tripsProcessor, IHubContext<DriverHub> driversHubContext)
        {
            _driversHubContext = driversHubContext;
            _sub = tripsProcessor.StartNewTripsSubscription(OnNewTripReceived, OnTripCompleted);
        }

        private void OnTripCompleted()
        {
            // TODO: implement
        }

        private void OnNewTripReceived(NewTrip newTrip, Driver driver)
        {
            _driversHubContext.Clients.User(driver.Id).SendAsync("NewTripRequest", newTrip.PassengerId).Wait();
        }
    }
}
