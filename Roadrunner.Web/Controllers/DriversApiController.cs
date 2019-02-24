using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Roadrunner.BusinessInterfaces;
using Roadrunner.Types;
using Roadrunner.Web.Hubs;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/drivers")]
    public class DriversApiController : ApiHubController<DriverHub>
    {
        private readonly IDriversProcessor _driversProcessor;

        public DriversApiController(IHubContext<DriverHub> hub, IDriversProcessor driversProcessor) : base(hub)
        {
            _driversProcessor = driversProcessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public ReqPositionModel Index()
        {
            return new ReqPositionModel { X = 1, Y = 10};
        }

        [HttpPut]
        [Authorize]
        public Task DriverReadyAtPosition(ReqPositionModel positionModel)
        {
            return _driversProcessor.DriverReadyAtPositionAsync(Position.Create(positionModel.X, positionModel.Y));
        }
    }
}