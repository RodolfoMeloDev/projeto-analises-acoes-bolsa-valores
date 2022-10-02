using App.CrossCutting.DependencyInjection;
using App.CrossCutting.Mappings;
using App.Data.Context;
using App.Data.Seed;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (Environment.GetEnvironmentVariable("INSERT_SEED") == "true")
{
    using (var service = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    using (var context = service.ServiceProvider.GetService<StockAnalysisContext>())
    {
        if (context != null)
        {
            DatasInitial.InsertDatasInitial(context);
        }        
    }
}

app.Run();
