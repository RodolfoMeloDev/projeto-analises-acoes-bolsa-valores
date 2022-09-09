using App.Domain.Interfaces.Services.Sector;
using App.Domain.Interfaces.Services.SubSector;
using App.Domain.Interfaces.Services.User;
using App.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ISectorService, SectorService>();
            serviceCollection.AddTransient<ISubSectorService, SubSectorService>();
        }
    }
}