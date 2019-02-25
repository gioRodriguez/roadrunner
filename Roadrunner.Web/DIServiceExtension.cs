using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Roadrunner.BusinessInterfaces;
using Roadrunner.BusinessLayer;
using Roadrunner.DataInterfaces;
using Roadrunner.DataLayer;
using Roadrunner.Utils.Identity;
using Roadrunner.Web.Hubs;

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
        }
    }
}
