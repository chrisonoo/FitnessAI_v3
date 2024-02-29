using MediatR;

namespace FitnessAI.Application.Workouts.Commands.EditWorkout;

public class AddWorkoutCommand : IRequest
{
    public string WorkoutTitle { get; set; }
    public string UserId { get; set; }
}