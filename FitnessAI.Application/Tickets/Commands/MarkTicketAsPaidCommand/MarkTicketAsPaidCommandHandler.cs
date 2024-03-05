using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommandHandler : IRequestHandler<MarkTicketAsPaidCommand>
{
    private readonly IApplicationDbContext _context;

    public MarkTicketAsPaidCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(MarkTicketAsPaidCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.SessionId, cancellationToken: cancellationToken);

        await UpdatePaymentInDb(ticket, cancellationToken);

        return Unit.Value;
    }

    private async Task UpdatePaymentInDb(Ticket ticket, CancellationToken cancellationToken)
    {
        ticket.IsPaid = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}