using MediatR;

namespace FitnessAI.Application.Exercises.Queries.GetExerciseDetails;

public class GetExerciseDetailsQuery : IRequest<ExerciseDetailsViewModel>
{
    public int ExerciseId { get; set; }
}