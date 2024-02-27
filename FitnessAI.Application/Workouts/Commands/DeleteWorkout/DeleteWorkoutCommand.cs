using MediatR;

namespace FitnessAI.Application.Workouts.Commands.EditWorkout;

public class DeleteWorkoutCommand : IRequest
{
    public int WorkoutId { get; set; }
}