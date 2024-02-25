using MediatR;

namespace FitnessAI.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, IEnumerable<WorkoutDto>>
{
    public Task<IEnumerable<WorkoutDto>> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
    {
        return new Task<IEnumerable<WorkoutDto>>(() => new List<WorkoutDto>());
    }
}