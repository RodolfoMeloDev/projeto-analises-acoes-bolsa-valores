using App.CrossCutting.DependencyInjection;
using App.CrossCutting.Mappings;
using App.Data.Context;
using App.Data.Seed;
using App.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new ModelToEntityProfile());
    cfg.AddProfile(new EntityToDtoProfile());
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
    paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().
                                    AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).
                                    RequireAuthenticatedUser().
                                    Build());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    s =>
    {
        s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Entre com o Token JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey

        });

        s.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    }
);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(opt => opt.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin());

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
