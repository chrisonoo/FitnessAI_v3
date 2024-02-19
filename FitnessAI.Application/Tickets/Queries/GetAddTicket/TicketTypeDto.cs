using FitnessAI.Domain.Enums;

namespace FitnessAI.Application.Tickets.Queries.GetAddTicket;

public class TicketTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public TicketTypeEnum TicketTypeEnum { get; set; }
}