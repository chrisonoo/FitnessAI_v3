using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class GetWorkoutsCalendarQuery : IRequest<IEnumerable<WorkoutCalendarDto>>
{
    public string UserId { get; set; }
}