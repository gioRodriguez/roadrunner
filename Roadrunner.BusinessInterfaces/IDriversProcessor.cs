using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.BusinessInterfaces
{
    public interface IDriversProcessor
    {
        Task DriverReadyAtPositionAsync(Position position);
        Task DriverPositionUpdateAsync(Position position);
    }
}
