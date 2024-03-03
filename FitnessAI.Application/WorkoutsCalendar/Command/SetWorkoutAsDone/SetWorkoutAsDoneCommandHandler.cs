using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.WorkoutsCalendar.Command.SetWorkoutAsDone;

public class SetWorkoutAsDoneCommandHandler : IRequestHandler<SetWorkoutAsDoneCommand>
{
    private readonly IApplicationDbContext _context;

    public SetWorkoutAsDoneCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SetWorkoutAsDoneCommand request, CancellationToken cancellationToken)
    {
        var workoutCalendar = await _context.WorkoutCalendars
            .Include(x => x.WorkoutCalendarExercises)
            .Where(x => x.UserId == request.UserId)
            .Where(x => x.IsActive)
            .FirstOrDefaultAsync(x => x.Date == DateTime.Parse(request.WorkoutDate), cancellationToken);

        if (workoutCalendar != null || request.WorkoutId == null) return Unit.Value;

        var workout = await _context.Workouts
            .Include(x => x.WorkoutExercises)
            .FirstOrDefaultAsync(x => x.Id == int.Parse(request.WorkoutId), cancellationToken);

        workoutCalendar = new WorkoutCalendar
        {
            UserId = request.UserId,
            Date = DateTime.Parse(request.WorkoutDate),
            Title = workout.Title,
            IsActive = true
        };
        _context.WorkoutCalendars.Add(workoutCalendar);
        await _context.SaveChangesAsync(cancellationToken);
        
        foreach (var workoutExercise in workout.WorkoutExercises)
        {
            var beginnerLoad = (await _context.UserExercises
                .Where(x => x.Id == workoutExercise.UserExerciseId)
                .FirstOrDefaultAsync(cancellationToken))
                .BeginnerLoad;
            
            var workoutCalendarExercise = new WorkoutCalendarExercise
            {
                WorkoutCalendarId = workoutCalendar.Id,
                UserExerciseId = workoutExercise.UserExerciseId,
                WorkoutLoad = beginnerLoad,
                IsActive = true
            };
            _context.WorkoutCalendarExercises.Add(workoutCalendarExercise);
        }
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}