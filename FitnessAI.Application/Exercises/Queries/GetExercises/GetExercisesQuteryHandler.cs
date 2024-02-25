using MediatR;

namespace FitnessAI.Application.Exercises.Queries.GetExercises;

public class GetExercisesQuteryHandler: IRequestHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    public Task<IEnumerable<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        return new Task<IEnumerable<ExerciseDto>>(() => new List<ExerciseDto>());
    }
}