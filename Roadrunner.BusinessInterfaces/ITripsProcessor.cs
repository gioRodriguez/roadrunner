using System;
using Roadrunner.Types;

namespace Roadrunner.BusinessInterfaces
{
    public interface ITripsProcessor
    {
        IDisposable StartNewTripsSubscription(Action<NewTrip, Driver> onNewTrip, Action onCompleted);
    }
}
