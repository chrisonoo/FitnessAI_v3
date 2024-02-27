using MediatR;

namespace FitnessAI.Application.Workouts.Commands.EditWorkout;

public class EditWorkoutCommand : IRequest
{
    public int WorkoutId { get; set; }
    public string WorkoutTitle { get; set; }
}