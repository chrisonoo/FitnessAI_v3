using FitnessAI.Application.Announcements.Extensions;
using FitnessAI.Application.Announcements.Queries.Dtos;
using FitnessAI.Application.Charts.Queries.Dtos;
using FitnessAI.Application.Common.Extensions;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Common.Models;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Clients.Queries.GetClientDashboard;

public class GetClientDashboardQueryHandler : IRequestHandler<GetClientDashboardQuery, GetClientDashboardVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public GetClientDashboardQueryHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<GetClientDashboardVm> Handle(GetClientDashboardQuery request, CancellationToken cancellationToken)
    {
        var vm = new GetClientDashboardVm();

        var user = await GetUser(request);

        vm.Email = user.Email;

        var ticket = GetActiveTicket(user);

        vm.TicketEndDate = GetTicketEndDate(ticket);
        vm.Announcements = await GetAnnouncements(request);

        var color = GetChartColors();
        var borderColors = GetChartBorderColors();

        vm.TrainingCountChart = GetTrainingCountChart(color, borderColors);
        vm.TheBestTrainingsChart = GetTheBestTrainingsChart(color, borderColors);

        return vm;
    }

    private ChartDto GetTheBestTrainingsChart(List<string> colors, List<string> borderColors)
    {
        var i = 0;

        return new ChartDto
        {
            Positions = new List<ChartPositionDto>
            {
                new() { Label = "Siłownia", Data = 855, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Crossfit", Data = 600, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Basen", Data = 350, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Aerobik", Data = 100, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Trójbój", Data = 8, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Rower", Data = 444, BorderColor = borderColors[i], Color = colors[i++] }
            }
        };
    }

    private ChartDto GetTrainingCountChart(
        List<string> colors, List<string> borderColors)
    {
        var i = 0;

        return new ChartDto
        {
            Label = "Ilość",
            Positions = new List<ChartPositionDto>
            {
                new() { Label = "Styczeń", Data = 4, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Luty", Data = 10, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Marzec", Data = 5, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Kwiecień", Data = 6, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Maj", Data = 8, BorderColor = borderColors[i], Color = colors[i++] },
                new() { Label = "Czerwiec", Data = 14, BorderColor = borderColors[i], Color = colors[i++] }
            }
        };
    }

    private List<string> GetChartBorderColors()
    {
        return new List<string>
        {
            "rgba(255, 99, 132, 1)",
            "rgba(54, 162, 235, 1)",
            "rgba(255, 206, 86, 1)",
            "rgba(75, 192, 192, 1)",
            "rgba(153, 102, 255, 1)",
            "rgba(255, 159, 64, 1)"
        };
    }

    private List<string> GetChartColors()
    {
        return new List<string>
        {
            "rgba(255, 99, 132, 0.2)",
            "rgba(54, 162, 235, 0.2)",
            "rgba(255, 206, 86, 0.2)",
            "rgba(75, 192, 192, 0.2)",
            "rgba(153, 102, 255, 0.2)",
            "rgba(255, 159, 64, 0.2)"
        };
    }

    private static DateTime? GetTicketEndDate(Ticket ticket)
    {
        return ticket == null ? null : ticket.EndDate;
    }

    private Ticket GetActiveTicket(ApplicationUser user)
    {
        return user.Tickets.FirstOrDefault(x =>
            x.StartDate.Date <= _dateTimeService.Now.Date &&
            x.EndDate.Date >= _dateTimeService.Now.Date);
    }

    private async Task<ApplicationUser> GetUser(GetClientDashboardQuery request)
    {
        return await _context
            .Users
            .Include(x => x.Tickets)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.UserId);
    }

    private async Task<PaginatedList<AnnouncementDto>> GetAnnouncements(GetClientDashboardQuery request)
    {
        return await _context.Announcements
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .Select(x => x.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}