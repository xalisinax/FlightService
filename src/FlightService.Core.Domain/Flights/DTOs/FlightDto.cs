namespace FlightService.Core.Domain.Flights.DTOs;

public class FlightDto
{
    public string Id { get; set; }
    public string Capacity { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Provider { get; set; }
    public string TakeOff { get; set; }
}
