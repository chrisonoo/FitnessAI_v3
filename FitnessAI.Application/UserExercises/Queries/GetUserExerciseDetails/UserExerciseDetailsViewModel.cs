using FitnessAI.Application.UserExercises.Queries.GetUserExercises;

namespace FitnessAI.Application.UserExercises.Queries.GetUserExerciseDetails;

public class UserExerciseDetailsViewModel
{
    public UserExerciseDto UserExercise { get; set; }
    public int NextUserExerciseId { get; set; }
    public int PreviousUserExerciseId { get; set; }
}