namespace FitnessAI.WebUI.Models.Api;

public class ApiCurrentUserDto
{
    public string Username { get; set; }
    public string AccessToken { get; set; }
    public string SelectedDate { get; set; }
    public int WorkoutCalendarId { get; set; }
}