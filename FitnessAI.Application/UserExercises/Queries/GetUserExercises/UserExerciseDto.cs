namespace FitnessAI.Application.UserExercises.Queries.GetUserExercises;

public class UserExerciseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string WorkoutInstruction { get; set; }
    public string BeginnerLoad { get; set; }
    public string IntermediateLoad { get; set; }
    public string AdvancedLoad { get; set; }
    public string ImageUrl { get; set; }
    public string MultimediaUrl { get; set; }
    public bool IsActive { get; set; }
}