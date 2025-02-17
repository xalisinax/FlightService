namespace FlightService.Api.V1.Models;

public class CreateFlightRequest
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public int Capacity { get; set; }
    public string Provider { get; set; }
    public DateTime TakeOffAt { get; set; }
}
