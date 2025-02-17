using FlightService.Core.Domain.Flights.QueryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightService.Infrastructure.Persistence.Flights.TypeConfigurations;

internal class FlightReservationQueryModelTypeConfiguration : IEntityTypeConfiguration<FlightReservationQueryModel>
{
    public void Configure(EntityTypeBuilder<FlightReservationQueryModel> builder)
    {
        builder.HasNoKey()
            .ToSqlQuery(View.FlightReservationQuery);
    }
}
