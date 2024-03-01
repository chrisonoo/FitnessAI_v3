using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.WorkoutsCalendar.Command.DeleteSelectedWorkout;

public class DeleteSelectedWorkoutCommandHandler : IRequestHandler<DeleteSelectedWorkoutCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSelectedWorkoutCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSelectedWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workoutCalendar = await _context.WorkoutCalendars
            .Include(x => x.WorkoutCalendarExercises)
            .Where(x => x.UserId == request.UserId && x.Date == DateTime.Parse(request.WorkoutDate))
            .FirstOrDefaultAsync(x => x.Id == int.Parse(request.WorkoutCalendarId), cancellationToken: cancellationToken);
        
        if (workoutCalendar == null) return Unit.Value;
        
        workoutCalendar.IsActive = false;
        foreach (var workoutCalendarExercise in workoutCalendar.WorkoutCalendarExercises)
        {
            workoutCalendarExercise.IsActive = false;
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}