using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.DataInterfaces
{
    public interface IHistoryRepository
    {
        Task DriverReadyAtPositionAsync(string driverId, Position position);
        Task DriverPositionUpdateAsync(string driverId, Position position);
        Task DriverTripAcceptedAsync(string driverId, string passengerId);
    }
}
