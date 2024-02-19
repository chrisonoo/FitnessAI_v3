using FitnessAI.Application.Invoices.Queries.GetInvoice;
using FitnessAI.Application.Invoices.Queries.GetInvoices;
using FitnessAI.Domain.Entities;

namespace FitnessAI.Application.Invoices.Extensions;

public static class InvoiceExtensions
{
    public static InvoiceBasicsDto ToBasicsDto(this Invoice invoice)
    {
        if (invoice == null)
            return null;

        return new InvoiceBasicsDto
        {
            Id = invoice.Id,
            UserId = invoice.UserId,
            CreatedDate = invoice.CreatedDate,
            Title = $"{invoice.Id}/{invoice.Month}/{invoice.Year}",
            UserName = $"{invoice.User.FirstName} {invoice.User.LastName}",
            Value = invoice.Value
        };
    }

    public static InvoiceDto ToDto(this Invoice invoice)
    {
        if (invoice == null)
            return null;

        return new InvoiceDto
        {
            Id = invoice.Id,
            UserId = invoice.UserId,
            CreatedDate = invoice.CreatedDate,
            Title = $"{invoice.Id}/{invoice.Month}/{invoice.Year}",
            UserName = $"{invoice.User.FirstName} {invoice.User.LastName}",
            Value = invoice.Value,
            MethodOfPayments = invoice.MethodOfPayment,
            TicketId = invoice.TicketId,
            PositionName = invoice.Ticket.TicketType.TicketTypeEnum.ToString(),
            UserEmail = invoice.User.Email,
            UserCity = $"{invoice.User.Address.ZipCode} {invoice.User.Address.City}",
            UserStreet = $"{invoice.User.Address.Street} {invoice.User.Address.StreetNumber}"
        };
    }
}