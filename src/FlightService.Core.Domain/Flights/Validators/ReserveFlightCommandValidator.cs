using FlightService.Core.Domain.Flights.Commands;
using FluentValidation;

namespace FlightService.Core.Domain.Flights.Validators;

public class ReserveFlightCommandValidator : AbstractValidator<ReserveFlightCommand>
{
    public ReserveFlightCommandValidator()
    {
        RuleFor(x => x.Seat)
            .NotEmpty()
            .MaximumLength(10);
    }
}
