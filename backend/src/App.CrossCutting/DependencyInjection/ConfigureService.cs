using App.Domain.Interfaces.Services.BaseTicker;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Interfaces.Services.Formula;
using App.Domain.Interfaces.Services.HistoryTicker;
using App.Domain.Interfaces.Services.Sector;
using App.Domain.Interfaces.Services.Segment;
using App.Domain.Interfaces.Services.SubSector;
using App.Domain.Interfaces.Services.Ticker;
using App.Domain.Interfaces.Services.User;
using App.Domain.Models.FilesImport;
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
            serviceCollection.AddTransient<ISegmentService, SegmentService>();
            serviceCollection.AddTransient<ITickerService, TickerService>();
            serviceCollection.AddTransient<IFileImportService, FileImportService>();
            serviceCollection.AddTransient<IHistoryTickerService, HistoryTickerService>();
            serviceCollection.AddTransient<IFilesService<FileStatusInvest>, FileStatusInvestService>();
            serviceCollection.AddTransient<IFilesService<FileFundamentus>, FileFundamentusService>();
            serviceCollection.AddTransient<IFormulaService, FormulaService>();
            serviceCollection.AddTransient<IBaseTickerService, BaseTickerService>();
        }
    }
}
