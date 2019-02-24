using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.DataInterfaces
{
    public interface IHistoryRepository
    {
        Task DriverReadyAtPositionAsync(string driverId, Position position);
    }
}
