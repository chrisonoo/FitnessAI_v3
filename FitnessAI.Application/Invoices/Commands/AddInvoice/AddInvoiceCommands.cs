using System.ComponentModel.DataAnnotations;
using MediatR;

namespace FitnessAI.Application.Invoices.Commands.AddInvoice;

public class AddInvoiceCommand : IRequest<int>
{
    [Required]
    public string TicketId { get; set; }

    public string UserId { get; set; }
}