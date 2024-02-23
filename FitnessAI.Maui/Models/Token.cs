using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class Token
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = default!;

    [JsonProperty("user_id")]
    public string UserId { get; set; } = default!;

    [JsonProperty("user_name")]
    public string UserName { get; set; } = default!;
}