using System.Threading.Tasks;
using Roadrunner.Web.Models;

namespace Roadrunner.Web.Hubs
{
    public interface IPassengersHub
    {
        Task TripRequest(ReqPositionModel origin, ReqPositionModel destiny);
    }
}
