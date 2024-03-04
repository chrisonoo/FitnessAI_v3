using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiWorkoutsViewModel
{
    [JsonProperty("next_date")]
    public DateTime NextDate { get; set; }

    [JsonProperty("previous_date")]
    public DateTime PreviousDate { get; set; }

    [JsonProperty("selected_date")]
    public DateTime SelectedDate { get; set; }

    [JsonProperty("workouts")]
    public List<ApiWorkoutsDto> Workouts { get; set; } = [];

    [JsonProperty("workout_calendar_id")]
    public int? WorkoutCalendarId { get; set; }

    [JsonProperty("workout_calendar_title")]
    public string? WorkoutCalendarTitle { get; set; }

    [JsonProperty("workout_calendar_exercises")]
    public List<ApiWorkoutCalendarExerciseDto>? WorkoutCalendarExercises { get; set; } = [];
}