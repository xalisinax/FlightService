namespace FlightService.Infrastructure.Persistence;

internal class View
{
    public const string FlightReservationQuery = @"
SELECT F.Id as FlightId,F.Origin,F.Destination,F.Provider,F.TakeOffAt,FP.UserId,FP.Seat 
FROM [dbo].[Flights] F INNER JOIN [dbo].[FlightPassenger] FP ON F.Id = FP.FlightId
";
}
