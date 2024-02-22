using FitnessAI.Application.Tickets.Queries.GetClientsTickets;
using FitnessAI.Application.Tickets.Queries.GetPrintTicket;
using FitnessAI.Domain.Entities;

namespace FitnessAI.Application.Tickets.Extensions;

public static class TicketExtensions
{
    public static TicketBasicsDto ToBasicsDto(this Ticket ticket)
    {
        if (ticket == null)
            return null;

        return new TicketBasicsDto
        {
            EndDate = ticket.EndDate.ToString("yyyy-MM-dd"),
            StartDate = ticket.StartDate.ToString("yyyy-MM-dd"),
            IsPaid = ticket.IsPaid,
            Id = ticket.Id
        };
    }

    public static PrintTicketDto ToPrintTicketDto(this Ticket ticket)
    {
        if (ticket == null)
            return null;

        return new PrintTicketDto
        {
            Id = ticket.Id,
            CompanyContactEmail = "onoomail@gmail.com",
            CompanyContactPhone = "500 500 500",
            EndDate = ticket.EndDate,
            StartDate = ticket.StartDate,
            FullName = $"{ticket.User.FirstName} {ticket.User.LastName}",
            Image = "images/gym-logo.jpg"
        };
    }
}