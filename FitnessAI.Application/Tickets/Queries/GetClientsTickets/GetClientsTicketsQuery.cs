﻿using FitnessAI.Application.Common.Models;
using MediatR;

namespace FitnessAI.Application.Tickets.Queries.GetClientsTickets;

public class GetClientsTicketsQuery : IRequest<PaginatedList<TicketBasicsDto>>
{
    public string UserId { get; set; }
    public string OrderInfo { get; set; }
    public string SearchValue { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}