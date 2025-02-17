using FlightService.Core.Domain.Flights.Commands;
using FluentValidation;

namespace FlightService.Core.Domain.Flights.Validators;

public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightCommandValidator()
    {
        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(20)
            .LessThanOrEqualTo(250);

        RuleFor(x => x.TakeOffAt)
            .GreaterThan(DateTime.UtcNow);

        RuleFor(x => x.Origin)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(256);

        RuleFor(x => x.Destination)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(256);

        RuleFor(x => x.Provider)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(256);
    }
}
