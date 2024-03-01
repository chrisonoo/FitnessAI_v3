using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class GetWorkoutsCalendarQueryHandler : IRequestHandler<GetWorkoutsCalendarQuery, WorkoutCalendarViewModel>
{
    public async Task<WorkoutCalendarViewModel> Handle(GetWorkoutsCalendarQuery request, CancellationToken cancellationToken)
    {
        var result = new WorkoutCalendarViewModel
        {
            PreviousDate = request.SelectedDate.AddDays(-1),
            SelectedDate = request.SelectedDate,
            NextDate = request.SelectedDate.AddDays(1)
        };
        
        await Task.CompletedTask;
        return result;
    }
}