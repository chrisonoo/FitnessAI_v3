using FitnessAI.Application.Exercises.Queries.GetExercises;

namespace FitnessAI.Application.Exercises.Queries.GetExerciseDetails;

public class ExerciseDetailsViewModel
{
    public ExerciseDto Exercise { get; set; }
    public int NextExerciseId { get; set; }
    public int PreviousExerciseId { get; set; }
    public string ActiveUserId { get; set; }
    public bool IsSelectedForActiveUser { get; set; }
}