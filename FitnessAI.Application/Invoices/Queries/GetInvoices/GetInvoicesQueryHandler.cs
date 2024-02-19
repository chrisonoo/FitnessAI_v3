using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Invoices.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, IEnumerable<InvoiceBasicsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetInvoicesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceBasicsDto>> Handle(GetInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = (await _context
                .Invoices
                .Include(x => x.User)
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .ToListAsync())
            .Select(x => x.ToBasicsDto());

        return invoices;
    }
}