using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roadrunner.Web.Hubs
{
    public class DriverHub : Hub<IDriverHub>
    {
        private const string DriverGrpName = "drivers";

        public Task Subscribe()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, DriverGrpName);
        }

        public Task Unsuscribe()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, DriverGrpName);
        }
    }
}
