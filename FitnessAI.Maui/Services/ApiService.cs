using System.Text;
using FitnessAI.Maui.Models;
using Newtonsoft.Json;

namespace FitnessAI.Maui.Services;

public static class ApiService
{
    private static readonly HttpClient HttpClient;
    
    static ApiService()
    {
        HttpClient = new HttpClient();
    }

    public static async Task<int> Login(string username, string password)
    {
        var login = new UserLoginDto { Username = username, Password = password };
        
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiusers/login", content);
        if (!response.IsSuccessStatusCode) return (int)response.StatusCode;
        
        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<TokenDto>(jsonResult);
        Preferences.Set("access_token", result!.AccessToken);
        Preferences.Set("user_id", result.UserId);
        Preferences.Set("user_name", result.UserName);
        Preferences.Set("first_name", result.FirstName);
        Preferences.Set("last_name", result.LastName);

        return (int)response.StatusCode;
    }
    
    public static async Task<CurrentUserDetailsViewModel> CurrentUserDetails()
    {
        var username = Preferences.Get("user_name", string.Empty);
        var token = Preferences.Get("access_token", string.Empty);
        var login = new CurrentUserLoginDto { Username = username, AccessToken = token };
        
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiusers/currentuserdetails", content);
        if (!response.IsSuccessStatusCode) return new CurrentUserDetailsViewModel();
        
        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CurrentUserDetailsViewModel>(jsonResult);
        return result!;
    }
}