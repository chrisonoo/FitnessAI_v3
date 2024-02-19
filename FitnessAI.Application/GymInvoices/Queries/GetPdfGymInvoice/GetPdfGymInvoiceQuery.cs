using MediatR;

namespace FitnessAI.Application.GymInvoices.Queries.GetPdfGymInvoice;

public class GetPdfGymInvoiceQuery : IRequest<InvoicePdfVm>
{
    public int Id { get; set; }
}