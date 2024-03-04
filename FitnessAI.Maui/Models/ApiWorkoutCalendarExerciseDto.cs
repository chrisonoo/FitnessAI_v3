using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiWorkoutCalendarExerciseDto
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; } = default!;
    [JsonProperty("category")]
    public string Category { get; set; } = default!;
    [JsonProperty("beginnerLoad")]
    public string BeginnerLoad { get; set; } = default!;
    [JsonProperty("intermediateLoad")]
    public string IntermediateLoad { get; set; } = default!;
    [JsonProperty("advancedLoad")]
    public string AdvancedLoad { get; set; } = default!;
    
}