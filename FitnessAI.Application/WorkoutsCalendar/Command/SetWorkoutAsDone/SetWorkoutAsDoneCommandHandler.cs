using System.Linq.Dynamic.Core;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.WorkoutsCalendar.Command.SetWorkoutAsDone;

public class SetWorkoutAsDoneCommandHandler :IRequestHandler<SetWorkoutAsDoneCommand>
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
            .FirstOrDefaultAsync(x => x.Date == DateTime.Parse(request.WorkoutDate), cancellationToken: cancellationToken);

        if (workoutCalendar != null) return Unit.Value;
        
        var workout = await _context.Workouts
            .Include(x => x.WorkoutExercises)
            .FirstOrDefaultAsync(x => x.Id == int.Parse(request.WorkoutId), cancellationToken: cancellationToken);
        
        workoutCalendar = new WorkoutCalendar
        {
            UserId = request.UserId, 
            Date = DateTime.Parse(request.WorkoutDate),
            Title = workout.Title,
            IsActive = true
        };
        _context.WorkoutCalendars.Add(workoutCalendar);
        await _context.SaveChangesAsync(cancellationToken);

        // var workoutCalendarExercise = workoutCalendar.WorkoutCalendarExercises
        //     .Where(x => x.WorkoutId == request.WorkoutId)
        //     .FirstOrDefault();
        //
        // if (workoutCalendarExercise == null)
        // {
        //     workoutCalendarExercise = new WorkoutCalendarExercise
        //     {
        //         WorkoutId = request.WorkoutId,
        //         WorkoutCalendarId = workoutCalendar.Id,
        //         IsDone = true
        //     };
        //     _context.WorkoutCalendarExercises.Add(workoutCalendarExercise);
        // }
        // else
        // {
        //     workoutCalendarExercise.IsDone = !workoutCalendarExercise.IsDone;
        // }



        return Unit.Value;
    }
}