using MediatR;

namespace FitnessAI.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, IEnumerable<WorkoutDto>>
{
    public async Task<IEnumerable<WorkoutDto>> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = new List<WorkoutDto>();
        return result;
    }
}