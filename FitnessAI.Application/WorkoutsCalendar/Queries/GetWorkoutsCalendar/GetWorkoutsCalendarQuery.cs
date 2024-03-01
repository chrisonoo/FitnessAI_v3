using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class GetWorkoutsCalendarQuery : IRequest<WorkoutCalendarViewModel>
{
    public string UserId { get; set; }
    public DateTime SelectedDate { get; set; }
}