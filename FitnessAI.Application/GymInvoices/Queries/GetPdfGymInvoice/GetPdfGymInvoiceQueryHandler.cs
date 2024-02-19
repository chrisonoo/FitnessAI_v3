using FitnessAI.Application.Common.Interfaces;
using MediatR;

namespace FitnessAI.Application.GymInvoices.Queries.GetPdfGymInvoice;

public class GetPdfGymInvoiceQueryHandler : IRequestHandler<GetPdfGymInvoiceQuery, InvoicePdfVm>
{
    private readonly IGymInvoices _gymInvoices;

    public GetPdfGymInvoiceQueryHandler(IGymInvoices gymInvoices)
    {
        _gymInvoices = gymInvoices;
    }

    public async Task<InvoicePdfVm> Handle(GetPdfGymInvoiceQuery request, CancellationToken cancellationToken)
    {
        return await _gymInvoices.GetPdfInvoice(request.Id);
    }
}