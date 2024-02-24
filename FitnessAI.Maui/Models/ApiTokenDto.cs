using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiTokenDto
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = default!;

    [JsonProperty("user_id")]
    public string UserId { get; set; } = default!;

    [JsonProperty("user_name")]
    public string UserName { get; set; } = default!;
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; } = default!;
    
    [JsonProperty("last_name")]
    public string LastName { get; set; } = default!;
}