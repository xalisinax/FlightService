namespace FlightService.Core.Domain.Flights.QueryModels;

public class FlightReservationQueryModel
{
    public string FlightId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Provider { get; set; }
    public DateTime TakeOffAt { get; set; }
    public string UserId { get; set; }
    public string Seat { get; set; }
}
