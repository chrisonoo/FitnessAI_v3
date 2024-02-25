using MediatR;

namespace FitnessAI.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQuery : IRequest<IEnumerable<WorkoutDto>>
{
    public string UserId { get; set; }
}