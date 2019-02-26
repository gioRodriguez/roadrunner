using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Utils.Identity;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Hubs
{
    public class DriverHub : Hub<IDriverHub>
    {
        private readonly IDriversProcessor _driversProcessor;
        private readonly IHubContext<PassengersHub> _passengersHub;
        private readonly IRoadrunnerIdentity _roadrunnerIdentity;

        public DriverHub(IDriversProcessor driversProcessor, IHubContext<PassengersHub> passengersHub, IRoadrunnerIdentity roadrunnerIdentity)
        {
            _driversProcessor = driversProcessor;
            _passengersHub = passengersHub;
            _roadrunnerIdentity = roadrunnerIdentity;
        }        

        [Authorize]
        public Task DriverReadyAtPosition(ReqPositionModel position)
        {
            return _driversProcessor.DriverReadyAtPositionAsync(Position.Create(position.X, position.Y));
        }

        [Authorize]
        public Task DriverPositionUpdate(ReqPositionModel position)
        {
            return _driversProcessor.DriverPositionUpdateAsync(Position.Create(position.X, position.Y));
        }

        [Authorize]
        public async Task DriverTripAccepted(string passengerId)
        {
            await _passengersHub.Clients.User(passengerId).SendAsync("DriverAssigned", _roadrunnerIdentity.GetUserId());
            await _driversProcessor.DriverTripAcceptedAsync(passengerId);
        }
    }
}
