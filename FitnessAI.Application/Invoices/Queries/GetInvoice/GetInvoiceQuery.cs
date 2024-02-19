using MediatR;

namespace FitnessAI.Application.Invoices.Queries.GetInvoice;

public class GetInvoiceQuery : IRequest<InvoiceDto>
{
    public int Id { get; set; }
    public string UserId { get; set; }
}