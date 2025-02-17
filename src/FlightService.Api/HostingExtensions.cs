using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Common.Pipelines.Commands;
using FlightService.Core.Domain.Roles.Entities;
using FlightService.Core.Domain.Users.Entities;
using FlightService.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace FlightService.Api;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configurations = builder.Configuration;

        services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<FlightDbContext>();

        services.AddControllers();

        services.AddOpenApi();

        services.AddCors(config =>
        {
            var origins = configurations.GetValue<string>("AllowedOrigins").Split(";");
            config.AddDefaultPolicy(policy =>
            {
                if (origins.Length > 0)
                    policy.WithOrigins(origins);
                else
                    policy.AllowAnyOrigin();

                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddDbContext<FlightDbContext>(options => options.UseSqlServer(configurations.GetConnectionString("Default")));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.Load("FlightService.Core.ApplicationServices"));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandPipeline<,>));
        });

        services.AddServicesWithTheirLifetimes(config =>
        {
            config.AssemblyNames = ["FlightService.Infrastructure.Persistence", "FlightService.Core.Domain", "FlightService.Core.ApplicationServices", "FlightService.Infrastructure.Provision"];
        });

        services.AddValidatorsFromAssembly(Assembly.Load("FlightService.Core.Domain"));

        services.AddAutoMapper(config =>
        {
            config.AddMaps(Assembly.Load("FlightService.MappingProfiles"));
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configurations.GetValue<string>("Idp");
                options.TokenValidationParameters.ValidateAudience = false;
            });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseHttpsRedirection();
        }



        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
