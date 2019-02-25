using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.BusinessLayer.DriverResolvers
{
    public interface IDriverResolverStrategy
    {
        Task<Driver> ResolverDriverForNewTripAsync(NewTrip newTrip);
    }
}
