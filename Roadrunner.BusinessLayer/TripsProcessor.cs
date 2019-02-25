using System;
using Roadrunner.BusinessInterfaces;
using Roadrunner.BusinessLayer.DriverResolvers;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.BusinessLayer
{
    public class TripsProcessor : ITripsProcessor
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly IDriverResolverStrategy _driverResolver;

        public TripsProcessor(ITripsRepository tripsRepository, IDriverResolverStrategy driverResolver)
        {
            _tripsRepository = tripsRepository;
            _driverResolver = driverResolver;
        }

        public IDisposable StartNewTripsSubscription(Action<NewTrip, Driver> onNewTrip, Action onCompleted)
        {
            return _tripsRepository.NewTrips
                .Subscribe(newTrip => { ResolverDriverForTrip(newTrip, onNewTrip); }, onCompleted);
        }

        private void ResolverDriverForTrip(NewTrip newTrip, Action<NewTrip, Driver> onNewTrip)
        {
            var driver = _driverResolver.ResolverDriverForNewTripAsync(newTrip).Result;
            onNewTrip(newTrip, driver);
        }
    }
}
