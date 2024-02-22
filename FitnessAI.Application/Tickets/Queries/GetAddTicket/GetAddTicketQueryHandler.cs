using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Commands.AddTicket;
using FitnessAI.Application.Tickets.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Tickets.Queries.GetAddTicket;

public class GetAddTicketQueryHandler : IRequestHandler<GetAddTicketQuery, AddTicketVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public GetAddTicketQueryHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddTicketVm> Handle(GetAddTicketQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddTicketVm();

        vm.Ticket = new AddTicketCommand
        {
            StartDate = _dateTimeService.Now,
            TicketTypeId = 3
        };

        vm.AvailableTicketTypes = await _context.TicketTypes
            .Include(x => x.Translations.Where(y => y.Language.Key == "pl"))
            .ThenInclude(x => x.Language)
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken: cancellationToken);

        return vm;
    }
}