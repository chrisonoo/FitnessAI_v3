using MediatR;

namespace FitnessAI.Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQuery : IRequest<IEnumerable<InvoiceBasicsDto>>
{
    public string UserId { get; set; }
}