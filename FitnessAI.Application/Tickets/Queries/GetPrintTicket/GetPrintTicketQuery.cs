using MediatR;

namespace FitnessAI.Application.Tickets.Queries.GetPrintTicket;

public class GetPrintTicketQuery : IRequest<PrintTicketDto>
{
    public string TicketId { get; set; }
    public string UserId { get; set; }
}