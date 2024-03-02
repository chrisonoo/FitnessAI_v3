using MediatR;

namespace FitnessAI.Application.UserExercises.Queries.GetUserExercises;

public class GetUserExercisesQuery : IRequest<IEnumerable<UserExerciseDto>>
{
    public string ActiveUserId { get; set; }
}