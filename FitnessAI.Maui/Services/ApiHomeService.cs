using System.Text;
using FitnessAI.Maui.Models;
using Newtonsoft.Json;

namespace FitnessAI.Maui.Services;

public abstract class ApiHomeService : ApiBaseService
{
    public static async Task<ApiHomeDto> Home()
    {
        var username = Preferences.Get("user_name", string.Empty);
        var token = Preferences.Get("access_token", string.Empty);
        var login = new ApiCurrentUserLoginDto { Username = username, AccessToken = token };

        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{AppSettings.ApiUrl}/api/apihome/home", content);
        if (!response.IsSuccessStatusCode) return new ApiHomeDto();

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiHomeDto>(jsonResult);

        return result!;
    }
}