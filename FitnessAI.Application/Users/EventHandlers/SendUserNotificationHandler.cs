using FitnessAI.Application.Common.Events;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Events;

namespace FitnessAI.Application.Users.EventHandlers;

public class SendUserNotificationHandler : IEventHandler<TicketPaidEvent>
{
    private readonly IUserNotificationService _userNotificationService;

    public SendUserNotificationHandler(IUserNotificationService userNotificationService)
    {
        _userNotificationService = userNotificationService;
    }

    public async Task HandleAsync(TicketPaidEvent @event)
    {
        await Task.Delay(2000);
        await _userNotificationService.SendNotification(@event.UserId,
            "Płatność została potwierdzona, dziękujemy za zakup karnetu.");
    }
}