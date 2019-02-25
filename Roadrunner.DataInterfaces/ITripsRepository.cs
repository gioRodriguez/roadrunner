using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using Roadrunner.Types;

namespace Roadrunner.DataInterfaces
{
    public interface ITripsRepository
    {
        ISubject<NewTrip> NewTrips { get; }
    }
}
