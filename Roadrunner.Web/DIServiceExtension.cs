using Microsoft.Extensions.DependencyInjection;
using Roadrunner.BusinessInterfaces;
using Roadrunner.BusinessLayer;
using Roadrunner.BusinessLayer.DriverResolvers;
using Roadrunner.DataInterfaces;
using Roadrunner.DataLayer;
using Roadrunner.Utils.Identity;
using Roadrunner.Web.MessagesRelays;

namespace Roadrunner.Web
{
    public static class DiServiceExtension
    {
        public static void AddDi(this IServiceCollection services)
        {
            services.AddTransient<IDriversProcessor, DriversProcessor>();
            services.AddTransient<IDriversRepository, DriversFakeRepository>();
            services.AddTransient<IHistoryRepository, HistoryRepository>();
            services.AddTransient<IRoadrunnerIdentity, RoadrunnerIdentity>();
            services.AddTransient<ITripsProcessor, TripsProcessor>();
            services.AddTransient<IDriverResolverStrategy, DemoDriverResolverStrategy>();

            services.AddSingleton<ITripsRepository, TripsFakeRepository>();
            services.AddSingleton<NewTripMessageRelay>();            
        }
    }
}
