using FlightService.Core.Domain.Flights.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Persistence.Flights.TypeConfigurations;

internal class FlightTypeConfiguration : IEntityTypeConfiguration<Flight>
{
    private const string TableName = "Flights";
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(256)
            .IsRequired()
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Origin)
            .HasMaxLength(256)
            .IsRequired()
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Destination)
            .HasMaxLength(256)
            .IsRequired()
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.TakeOffAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        var passengerBuilder = builder.OwnsMany(x => x.Passengers);

        passengerBuilder.Property(x => x.UserId)
            .HasMaxLength(32)
            .IsRequired();

        passengerBuilder.Property(x => x.Seat)
            .IsRequired()
            .HasMaxLength(10);

        passengerBuilder.Property(x => x.ReservedAt)
            .IsRequired();


        builder.HasIndex(x => new { x.Origin, x.Destination, x.Provider });
    }
}
