using System.Reactive.Subjects;
using System.Timers;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.DataLayer
{
    public class TripsFakeRepository : ITripsRepository
    {
        public ISubject<NewTrip> NewTrips { get; }

        private readonly Timer _timer;

        public TripsFakeRepository()
        {
            NewTrips = new Subject<NewTrip>();
            _timer = new Timer(1000);
            _timer.Elapsed += CheckQueue;
            _timer.Enabled = true;
        }

        private void CheckQueue(object sender, ElapsedEventArgs e)
        {
            var newTrip = FakeMemoryTripsQueue.Dequeue();
            if (newTrip != null)
            {
                NewTrips.OnNext(newTrip);
            }
        }        
    }
}
