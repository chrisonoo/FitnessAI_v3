using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.Application.Invoices.Queries.GetPdfInvoice;

public class GetPdfInvoiceQuery : IRequest<InvoicePdfVm>
{
    public int InvoiceId { get; set; }
    public string UserId { get; set; }
    public ActionContext Context { get; set; }
}