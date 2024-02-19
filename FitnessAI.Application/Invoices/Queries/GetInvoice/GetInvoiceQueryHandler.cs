using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Invoices.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Invoices.Queries.GetInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceDto>
{
    private readonly IApplicationDbContext _context;

    public GetInvoiceQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var invoice = (await _context
                .Invoices
                .Include(x => x.Ticket).ThenInclude(x => x.TicketType)
                .Include(x => x.User).ThenInclude(x => x.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId))
            .ToDto();

        return invoice;
    }
}