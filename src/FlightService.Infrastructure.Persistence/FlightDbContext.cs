using FlightService.Core.Domain.Common.Domain;
using FlightService.Core.Domain.Flights.Entities;
using FlightService.Core.Domain.Flights.QueryModels;
using FlightService.Core.Domain.Roles.Entities;
using FlightService.Core.Domain.Users.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlightService.Infrastructure.Persistence;

public class FlightDbContext(DbContextOptions dbContextOptions, IPublisher publisher) : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(dbContextOptions)
{
    private readonly IPublisher _publisher = publisher;

    public DbSet<Flight> Flights { get; set; }
    public DbSet<FlightReservationQueryModel> FlightReservationQueryModels { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        try
        {
            await PublishDomainEventsAsync();
        }
        catch { }

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
