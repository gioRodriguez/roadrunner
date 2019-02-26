using System.Reactive.Subjects;
using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.DataInterfaces
{
    public interface ITripsRepository
    {
        ISubject<NewTrip> NewTrips { get; }
        Task TripRequestAsync(string userId, Position origin, Position destiny);
    }
}
