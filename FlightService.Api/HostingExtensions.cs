using FlighService.Core.Domain.Common.DI;
using FlighService.Core.Domain.Common.Pipelines.Commands;
using FlightService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlightService.Api;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configurations = builder.Configuration;

        services.AddControllers();

        services.AddOpenApi();

        services.AddServicesWithTheirLifetimes(config =>
        {
            config.AssemblyNames = ["FlightService.Infrastructure.Persistance", "FlightService.Core.Domain", "FlightService.Core.ApplicationServices"];
        });

        services.AddDbContext<CommandDbContext>(options => options.UseSqlServer(configurations.GetConnectionString("Default")));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.Load("FlightService.Core.ApplicationServices"));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandPipeline<,>));
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
