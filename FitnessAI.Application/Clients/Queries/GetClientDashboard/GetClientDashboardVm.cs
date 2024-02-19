using FitnessAI.Application.Announcements.Queries.Dtos;
using FitnessAI.Application.Charts.Queries.Dtos;
using FitnessAI.Application.Common.Models;

namespace FitnessAI.Application.Clients.Queries.GetClientDashboard;

public class GetClientDashboardVm
{
    public string Email { get; set; }
    public DateTime? TicketEndDate { get; set; }
    public PaginatedList<AnnouncementDto> Announcements { get; set; }
    public ChartDto TrainingCountChart { get; set; }
    public ChartDto TheBestTrainingsChart { get; set; }
}