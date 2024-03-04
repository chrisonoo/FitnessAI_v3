namespace FitnessAI.Maui.Models;

public class ApiCurrentUserLoginDto
{
    public string Username { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string SelectedDate { get; set; } = default!;
    public int WorkoutCalendarId { get; set; }
}