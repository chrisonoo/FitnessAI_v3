using MediatR;

namespace FitnessAI.Application.Exercises.Queries.GetExercises;

public class GetExercisesQuteryHandler: IRequestHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    public async Task<IEnumerable<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = new List<ExerciseDto>();
        return result;
    }
}