using Duende.IdentityServer;
using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Common.Pipelines.Commands;
using FlightService.Core.Domain.Roles.Entities;
using FlightService.Core.Domain.Users.Entities;
using FlightService.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace FlightService.Idp
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configurations = builder.Configuration;

            services.AddRazorPages();

            services.AddDbContext<FlightDbContext>(options =>
                options.UseSqlServer(configurations.GetConnectionString("Default")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<FlightDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddIdentityServer(options =>
                {
                    options.Authentication.CookieSameSiteMode = SameSiteMode.Unspecified;
                    options.Authentication.CheckSessionCookieSameSiteMode = SameSiteMode.Unspecified;

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<User>();

            services.AddAuthentication();

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

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}