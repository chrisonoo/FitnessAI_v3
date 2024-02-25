using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiWorkoutsViewModel
{
    [JsonProperty("test_string")]
    public string TestString { get; set; } = default!;
}