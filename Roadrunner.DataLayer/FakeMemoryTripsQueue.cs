using System.Collections.Concurrent;
using Roadrunner.Types;

namespace Roadrunner.DataLayer
{
    public static class FakeMemoryTripsQueue
    {
        private static readonly ConcurrentQueue<NewTrip> newTripsQueue = new ConcurrentQueue<NewTrip>();

        public static void Enqueue(NewTrip newTrip)
        {
            newTripsQueue.Enqueue(newTrip);
        }

        public static NewTrip Dequeue()
        {
            newTripsQueue.TryDequeue(out var newTrip);
            return newTrip;
        }
    }
}
