using MediatR;

namespace FitnessAI.Application.Tickets.Queries.GetTicketById;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        //pobieranie danych z bazy danych
        //dao

        await Task.CompletedTask;

        return new TicketDto { Id = request.Id, Name = "Name" };
    }
}