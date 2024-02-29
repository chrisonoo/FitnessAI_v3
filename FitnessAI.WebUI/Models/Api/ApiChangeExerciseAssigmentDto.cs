namespace FitnessAI.WebUI.Models.Api;

public class ApiChangeExerciseAssigmentDto
{
    public int ExerciseId { get; set; }
    public int WorkoutId { get; set; }
    public bool IsAssigned { get; set; }
}