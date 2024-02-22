using MediatR;

namespace FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommand : IRequest
{
    public string SessionId { get; set; }
}