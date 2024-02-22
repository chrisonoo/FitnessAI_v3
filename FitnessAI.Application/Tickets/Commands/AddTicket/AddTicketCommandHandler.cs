using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Tickets.Commands.AddTicket;

public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public AddTicketCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<string> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        var sessionId = Guid.NewGuid().ToString();
        var token = $"test-payment-token-{Guid.NewGuid().ToString()}";
        await AddToDatabase(request, sessionId, token, cancellationToken);
        
        // Symulacja potwierdzenia płatności od systemu obsługującego płatności
        // Po stronie widoku Tickets.cshtml
        // API TicketApiController.cs wywołane z poziomu JS w przeglądarce

        return sessionId;
    }

    private async Task AddToDatabase(AddTicketCommand request, string sessionId, string token,
        CancellationToken cancellationToken)
    {
        var ticketType = _context.TicketTypes
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == request.TicketTypeId);

        var ticket = new Ticket
        {
            Id = sessionId,
            StartDate = request.StartDate,
            EndDate = CalculateEndDate(request.StartDate, ticketType!.TicketTypeEnum),
            TicketTypeId = request.TicketTypeId,
            Price = ticketType!.Price,
            Token = token,
            CreatedDate = _dateTimeService.Now,
            UserId = request.UserId
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static DateTime CalculateEndDate(DateTime requestStartDate, TicketTypeEnum ticketTypeEnum)
    {
        return ticketTypeEnum switch
        {
            TicketTypeEnum.Single 
                => requestStartDate,
            TicketTypeEnum.Weekly
                => requestStartDate.AddDays(6).Date,
            TicketTypeEnum.Monthly 
                => requestStartDate.AddDays(DateTime.DaysInMonth(requestStartDate.Year, requestStartDate.Month) - 1).Date,
            _ => requestStartDate.AddDays(364).Date
        };
    }
}