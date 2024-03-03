using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Workouts.Queries.GetWorkouts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class GetWorkoutsCalendarQueryHandler : IRequestHandler<GetWorkoutsCalendarQuery, WorkoutCalendarViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetWorkoutsCalendarQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<WorkoutCalendarViewModel> Handle(GetWorkoutsCalendarQuery request, CancellationToken cancellationToken)
    {
        var result = new WorkoutCalendarViewModel
        {
            PreviousDate = request.SelectedDate.AddDays(-1),
            SelectedDate = request.SelectedDate,
            NextDate = request.SelectedDate.AddDays(1),
            Workouts = await _context.Workouts
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsActive)
                .Select(x => new WorkoutDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    UserId = x.UserId
                })
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken: cancellationToken),
            WorkoutCalendar = await _context.WorkoutCalendars
                .Include(x => x.WorkoutCalendarExercises)
                .ThenInclude(x => x.UserExercise)
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.Date == request.SelectedDate)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
        };
        
        await Task.CompletedTask;
        return result;
    }
}