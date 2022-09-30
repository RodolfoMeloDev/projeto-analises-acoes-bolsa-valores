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
            serviceCollection.AddScoped<ISegmentRepository, SegmentImplementation>();
            serviceCollection.AddScoped<ITickerRepository, TickerImplementation>();
            serviceCollection.AddScoped<IFileImportRepository, FileImportImplementation>();
            serviceCollection.AddScoped<IHistoryTickerRepository, HistoryTickerImplementation>();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi localizado a connectionString do sistema.");

            serviceCollection.AddDbContext<AnaliseDeAcoesContext>(
                    options => options.UseNpgsql(connectionString)
                );
        }
    }
}
