using FitnessAI.Application.Common.Events;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Common.Models.Payments;
using FitnessAI.Application.Tickets.Events;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommandHandler : IRequestHandler<MarkTicketAsPaidCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly ILogger _logger;
    private readonly IPrzelewy24 _przelewy24;

    public MarkTicketAsPaidCommandHandler(
        IApplicationDbContext context,
        IPrzelewy24 przelewy24,
        ILogger<MarkTicketAsPaidCommandHandler> logger,
        IEventDispatcher eventDispatcher)
    {
        _context = context;
        _przelewy24 = przelewy24;
        _logger = logger;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Unit> Handle(MarkTicketAsPaidCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Przelewy24 - payment verification started - {request.SessionId}");

        await VerifyTransactionPrzelewy24(request);

        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.SessionId);

        await UpdatePaymentInDb(ticket, cancellationToken);

        _logger.LogInformation($"Przelewy24 - payment verification finished - {request.SessionId}");

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

    private async Task VerifyTransactionPrzelewy24(MarkTicketAsPaidCommand request)
    {
        var response = await _przelewy24.TransactionVerifyAsync(new P24TransactionVerifyRequest
        {
            Amount = request.Amount,
            Currency = request.Currency,
            MerchantId = request.MerchantId,
            PosId = request.PosId,
            OrderId = request.OrderId,
            SessionId = request.SessionId
        });

        if (response.Data?.Status != "success")
            throw new Exception("Invalid payment status in przelewy24");
    }
}