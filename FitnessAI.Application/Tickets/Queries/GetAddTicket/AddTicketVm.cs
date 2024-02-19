using FitnessAI.Application.Tickets.Commands.AddTicket;

namespace FitnessAI.Application.Tickets.Queries.GetAddTicket;

public class AddTicketVm
{
    public AddTicketCommand Ticket { get; set; }
    public List<TicketTypeDto> AvailableTicketTypes { get; set; }
}