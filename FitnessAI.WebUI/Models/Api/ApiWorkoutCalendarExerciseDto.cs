namespace FitnessAI.WebUI.Models.Api;

public class ApiWorkoutCalendarExerciseDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Title { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string BeginnerLoad { get; set; } = default!;
    public string IntermediateLoad { get; set; } = default!;
    public string AdvancedLoad { get; set; } = default!;
}