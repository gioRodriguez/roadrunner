using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Hubs
{
    public class DriverHub : Hub<IDriverHub>
    {
        private readonly IDriversProcessor _driversProcessor;

        public DriverHub(IDriversProcessor driversProcessor)
        {
            _driversProcessor = driversProcessor;
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
        public Task DriverTripAccepted()
        {
            return _driversProcessor.DriverTripAcceptedAsync();
        }
    }
}
