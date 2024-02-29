namespace FitnessAI.WebUI.Models.Api;

public class ApiWorkoutExerciseDto
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseTitle { get; set; }
    public string ExerciseCategory { get; set; }
}