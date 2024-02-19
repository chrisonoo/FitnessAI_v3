using FitnessAI.Application.Common.Events;

namespace FitnessAI.Application.Tickets.Events;

public class TicketPaidEvent : IEvent
{
    public string TicketId { get; set; }
    public string UserId { get; set; }
}