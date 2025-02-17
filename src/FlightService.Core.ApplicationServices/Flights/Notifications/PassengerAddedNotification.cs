using FlightService.Core.Domain.Common.Providers.EMail;
using FlightService.Core.Domain.Flights.Events;
using FlightService.Core.Domain.Users.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlightService.Core.ApplicationServices.Flights.Notifications;

public class PassengerAddedNotification(UserManager<User> userManager, IEmailProvider emailProvider) : INotificationHandler<FlightPassengerAdded>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IEmailProvider _emailProvider = emailProvider;

    public async Task Handle(FlightPassengerAdded notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.Id);

        await _emailProvider.Send(user.Email, nameof(FlightPassengerAdded), $"Your reservation at seat : {notification.Seat}");
    }
}
