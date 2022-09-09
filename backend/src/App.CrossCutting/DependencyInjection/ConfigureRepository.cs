using App.Data.Context;
using App.Data.Implementations;
using App.Data.Repository;
using App.Domain.Interfaces.Services;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            // Banco de dados espera sempre um serviço de scopo
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddScoped<ISectorRepository, SectorImplementation>();
            serviceCollection.AddScoped<ISubSectorRepository, SubSectorImplementation>();

            serviceCollection.AddDbContext<AnaliseDeAcoesContext>(
                    options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
        }
    }
}