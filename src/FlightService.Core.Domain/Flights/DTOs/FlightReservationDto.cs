namespace FlightService.Core.Domain.Flights.DTOs;

public class FlightReservationDto
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Provider { get; set; }
    public string TakeOffAt { get; set; }
    public string Seat { get; set; }
}
