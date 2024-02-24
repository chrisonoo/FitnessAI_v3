using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiAnnouncementsDto
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; } = default!;
}