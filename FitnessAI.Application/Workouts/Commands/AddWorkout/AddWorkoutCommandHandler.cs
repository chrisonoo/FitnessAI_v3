using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;

namespace FitnessAI.Application.Workouts.Commands.EditWorkout;

public class AddWorkoutCommandHandler : IRequestHandler<AddWorkoutCommand>
{
    private readonly IApplicationDbContext _context;

    public AddWorkoutCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = new Workout
        {
            Title = request.WorkoutTitle,
            IsActive = true,
            UserId = request.UserId
        };
        
        _context.Workouts.Add(workout);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}