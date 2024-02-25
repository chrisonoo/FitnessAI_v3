using MediatR;

namespace FitnessAI.Application.Exercises.Queries.GetExercises;

public class GetExercisesQuery : IRequest<IEnumerable<ExerciseDto>>
{
    public string UserId { get; set; }
}