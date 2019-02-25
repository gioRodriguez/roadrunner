using System.Threading.Tasks;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Hubs
{
    public interface IDriverHub
    {
        Task DriverReadyAtPosition(ReqPositionModel position);
        Task DriverPositionUpdate(ReqPositionModel position);
    }
}
