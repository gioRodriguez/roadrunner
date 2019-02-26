using System;
using System.Threading.Tasks;
using Roadrunner.BusinessInterfaces;
using Roadrunner.BusinessLayer.DriverResolvers;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;
using Roadrunner.Utils.Identity;

namespace Roadrunner.BusinessLayer
{
    public class TripsProcessor : ITripsProcessor
    {
        private readonly IRoadrunnerIdentity _roadrunnerIdentity;
        private readonly ITripsRepository _tripsRepository;
        private readonly IDriverResolverStrategy _driverResolver;

        public TripsProcessor(IRoadrunnerIdentity roadrunnerIdentity, ITripsRepository tripsRepository, IDriverResolverStrategy driverResolver)
        {
            _roadrunnerIdentity = roadrunnerIdentity;
            _tripsRepository = tripsRepository;
            _driverResolver = driverResolver;
        }

        public IDisposable StartNewTripsSubscription(Action<NewTrip, Driver> onNewTrip, Action onCompleted)
        {
            return _tripsRepository.NewTrips
                .Subscribe(newTrip => { ResolverDriverForTrip(newTrip, onNewTrip); }, onCompleted);
        }

        public Task TripRequestAsync(Position origin, Position destiny)
        {
            _roadrunnerIdentity.ThrowUnautorizedIfNotAuthenticated();

            return _tripsRepository.TripRequestAsync(_roadrunnerIdentity.GetUserId(), origin, destiny);
        }

        private void ResolverDriverForTrip(NewTrip newTrip, Action<NewTrip, Driver> onNewTrip)
        {
            var driver = _driverResolver.ResolverDriverForNewTripAsync(newTrip).Result;
            if (driver == null)
            {
                // if driver not found, ask the trip again
                _tripsRepository.TripRequestAsync(newTrip.PassengerId, newTrip.Origin, newTrip.Destiny).Wait();
                return;
            }

            onNewTrip(newTrip, driver);
        }
    }
}
