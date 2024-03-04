using System.Text;
using FitnessAI.Maui.Models;
using Newtonsoft.Json;

namespace FitnessAI.Maui.Services;

public class ApiWorkoutService : ApiBaseService
{
    public static async Task<ApiWorkoutsViewModel> Workouts(string date)
    {
        var username = Preferences.Get("user_name", string.Empty);
        var token = Preferences.Get("access_token", string.Empty);
        var login = new ApiCurrentUserLoginDto { Username = username, AccessToken = token, SelectedDate = date};

        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiworkouts/workouts", content);
        if (!response.IsSuccessStatusCode) return new ApiWorkoutsViewModel();

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiWorkoutsViewModel>(jsonResult);
        
        return result!;
    }
    
    public static async Task<ApiWorkoutsViewModel> DeleteWorkout()
    {
        var username = Preferences.Get("user_name", string.Empty);
        var token = Preferences.Get("access_token", string.Empty);
        var workoutCalendarId = Preferences.Get("workout_calendar_id", 0);
        var workoutSelectedDate = Preferences.Get("workout_selected_date", string.Empty);
        var login = new ApiCurrentUserLoginDto { Username = username, AccessToken = token, SelectedDate = workoutSelectedDate, WorkoutCalendarId = workoutCalendarId};

        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiworkouts/deleteworkout", content);
        if (!response.IsSuccessStatusCode) return new ApiWorkoutsViewModel();

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiWorkoutsViewModel>(jsonResult);
        
        return result!;
    }
    
    public static async Task<ApiWorkoutsViewModel> AddWorkout(int workoutCalendarId)
    {
        var username = Preferences.Get("user_name", string.Empty);
        var token = Preferences.Get("access_token", string.Empty);
        var workoutSelectedDate = Preferences.Get("workout_selected_date", string.Empty);
        var login = new ApiCurrentUserLoginDto { Username = username, AccessToken = token, SelectedDate = workoutSelectedDate, WorkoutCalendarId = workoutCalendarId};

        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiworkouts/addworkout", content);
        if (!response.IsSuccessStatusCode) return new ApiWorkoutsViewModel();

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiWorkoutsViewModel>(jsonResult);
        
        return result!;
    }
}