using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiHomeDto
{
    [JsonProperty("ticket_end_date")]
    public DateTime TicketEndDate { get; set; }
    
    [JsonProperty("announcements")]
    public List<ApiAnnouncementsDto> Announcements { get; set; } = default!;
}