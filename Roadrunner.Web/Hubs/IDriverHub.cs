using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.Web.Hubs
{
    public interface IDriverHub
    {
        Task SetConnectionId(string connectionId);
        Task ReportDriverPosition(Position driverPosition);
    }
}
