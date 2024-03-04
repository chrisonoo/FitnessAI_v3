using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiWorkoutsDto
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; } = default!;
    [JsonProperty("userId")]
    public string UserId { get; set; } = default!;
}