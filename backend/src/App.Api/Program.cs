using System.Text.Json;
using App.CrossCutting.DependencyInjection;
using App.CrossCutting.Mappings;
using App.Data.Context;
using App.Data.Seed;
using App.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var configLog = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
   .Build();

LogManager.Configuration = new NLogLoggingConfiguration(configLog.GetSection("NLog"));

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

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
    authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.SaveToken = true;
    bearerOptions.RequireHttpsMetadata = false;

    bearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        RequireExpirationTime = false,
        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
        {
            return notBefore <= DateTime.UtcNow &&
            expires >= DateTime.UtcNow;
        },
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = Environment.GetEnvironmentVariable("Audience"),
        ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
        IssuerSigningKey = signingConfigurations.Key
    };

    bearerOptions.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = c =>
        {
            c.NoResult();
            c.Response.StatusCode = 401;
            c.Response.ContentType = "application/json";
            c.Response.WriteAsync(JsonSerializer.Serialize(new { StatusCode = 401, Message = "Token inválido", MessageException = c.Exception.Message }));
            return Task.CompletedTask;
        },
        OnForbidden = c =>
        {
            c.NoResult();
            c.Response.StatusCode = 403;
            c.Response.ContentType = "application/json";
            c.Response.WriteAsync(JsonSerializer.Serialize(new { StatusCode = 403, Message = "Forbidden" }));
            return Task.CompletedTask;
        },
        OnChallenge = c =>
        {
            c.HandleResponse();
            return Task.CompletedTask;
        }
    };
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

builder.Services.AddMvc(opt => opt.EnableEndpointRouting = false);

//builder.Logging.ClearProviders();
builder.Host.UseNLog();

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

app.UseMvc();

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
