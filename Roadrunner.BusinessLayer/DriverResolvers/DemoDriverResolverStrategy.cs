using System;
using System.Threading.Tasks;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;

namespace Roadrunner.BusinessLayer.DriverResolvers
{
    public class DemoDriverResolverStrategy : IDriverResolverStrategy
    {
        private readonly IDriversRepository _driversRepository;

        public DemoDriverResolverStrategy(IDriversRepository driversRepository)
        {
            _driversRepository = driversRepository;
        }

        public Task<Driver> ResolverDriverForNewTripAsync(NewTrip newTrip)
        {
            return _driversRepository.GetRandomDriverAsync();
        }
    }
}
