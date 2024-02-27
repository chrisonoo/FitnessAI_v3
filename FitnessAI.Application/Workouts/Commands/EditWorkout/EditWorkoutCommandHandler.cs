using FitnessAI.Application.Common.Interfaces;
using MediatR;

namespace FitnessAI.Application.Workouts.Commands.EditWorkout;

public class EditWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand>
{
    private readonly IApplicationDbContext _context;

    public EditWorkoutCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _context.Workouts.FindAsync(request.WorkoutId);

        if (workout != null) workout.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}