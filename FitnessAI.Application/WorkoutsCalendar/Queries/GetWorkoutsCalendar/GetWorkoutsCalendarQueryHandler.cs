using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class GetWorkoutsCalendarQueryHandler : IRequestHandler<GetWorkoutsCalendarQuery, IEnumerable<WorkoutCalendarDto>>
{
    public async Task<IEnumerable<WorkoutCalendarDto>> Handle(GetWorkoutsCalendarQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = new List<WorkoutCalendarDto>();
        return result;
    }
}