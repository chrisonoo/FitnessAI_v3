using MediatR;

namespace FitnessAI.Application.UserExercises.Queries.GetUserExerciseDetails;

public class GetUserExerciseDetailsQuery : IRequest<UserExerciseDetailsViewModel>
{
    public int UserExerciseId { get; set; }
    public string ActiveUserId { get; set; }
}