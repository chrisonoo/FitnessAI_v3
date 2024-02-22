using FitnessAI.Application.Common.Events;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Events;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommandHandler : IRequestHandler<MarkTicketAsPaidCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventDispatcher _eventDispatcher;

    public MarkTicketAsPaidCommandHandler(
        IApplicationDbContext context,
        IEventDispatcher eventDispatcher)
    {
        _context = context;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Unit> Handle(MarkTicketAsPaidCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.SessionId, cancellationToken: cancellationToken);

        await UpdatePaymentInDb(ticket, cancellationToken);

        await _eventDispatcher.PublishAsync(new TicketPaidEvent
        {
            TicketId = ticket.Id,
            UserId = ticket.UserId
        });

        return Unit.Value;
    }

    private async Task UpdatePaymentInDb(Ticket ticket, CancellationToken cancellationToken)
    {
        ticket.IsPaid = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}