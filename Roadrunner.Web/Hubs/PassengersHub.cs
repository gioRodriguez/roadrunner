using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Hubs
{
    public class PassengersHub : Hub<IPassengersHub>
    {
        private readonly ITripsProcessor _tripsProcessor;

        public PassengersHub(ITripsProcessor tripsProcessor)
        {
            _tripsProcessor = tripsProcessor;
        }

        [Authorize]
        public Task TripRequest(ReqPositionModel origin, ReqPositionModel destiny)
        {
            return _tripsProcessor.TripRequestAsync(
                Position.Create(origin.X, origin.Y),
                Position.Create(destiny.X, destiny.Y)
            );
        }
    }
}
