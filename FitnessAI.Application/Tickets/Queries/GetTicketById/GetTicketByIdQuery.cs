using MediatR;

namespace FitnessAI.Application.Tickets.Queries.GetTicketById;

public class GetTicketByIdQuery : IRequest<TicketDto>
{
    public int Id { get; set; }
}