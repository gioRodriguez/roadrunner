using System;
using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.BusinessInterfaces
{
    public interface ITripsProcessor
    {
        IDisposable StartNewTripsSubscription(Action<NewTrip, Driver> onNewTrip, Action onCompleted);
        Task TripRequestAsync(Position origin, Position destiny);
    }
}
