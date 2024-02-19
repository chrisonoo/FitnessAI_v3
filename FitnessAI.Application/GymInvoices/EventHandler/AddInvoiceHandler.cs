using FitnessAI.Application.Common.Events;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Events;

namespace FitnessAI.Application.GymInvoices.EventHandler;

public class AddInvoiceHandler : IEventHandler<TicketPaidEvent>
{
    private readonly IGymInvoices _gymInvoices;

    public AddInvoiceHandler(IGymInvoices gymInvoices)
    {
        _gymInvoices = gymInvoices;
    }

    public async Task HandleAsync(TicketPaidEvent @event)
    {
        await _gymInvoices.AddInvoice(@event.TicketId, @event.UserId);
    }
}