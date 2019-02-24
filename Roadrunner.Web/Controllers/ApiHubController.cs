using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Roadrunner.Web.Controllers
{
    public abstract class ApiHubController<T> : Controller where T : Hub
    {
        private readonly IHubContext<T> _hub;

        protected ApiHubController(
            IHubContext<T> hub
        )
        {
            _hub = hub;
        }
    }
}