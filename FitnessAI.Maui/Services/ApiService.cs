using System.Text;
using FitnessAI.Maui.Models;
using Newtonsoft.Json;

namespace FitnessAI.Maui.Services;

public static class ApiService
{
    private static readonly HttpClient HttpClient;
    
    static ApiService()
    {
        // var handler = new HttpClientHandler();
        // handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        // HttpClient = new HttpClient(handler);
        HttpClient = new HttpClient();
    }

    public static async Task<bool> Login(string email, string password)
    {
        var login = new LoginViewModel { Email = email, Password = password };
        
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apiusers/login", content);
        if (!response.IsSuccessStatusCode) return false;
        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Token>(jsonResult);
        Preferences.Set("access_token", result!.AccessToken);
        Preferences.Set("user_id", result.UserId);
        Preferences.Set("user_name", result.UserName);

        return true;
    }
}