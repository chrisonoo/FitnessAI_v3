using MediatR;

namespace FitnessAI.Application.Invoices.Commands.DeleteInvoice;

public class DeleteInvoiceCommand : IRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
}