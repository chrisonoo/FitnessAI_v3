namespace FitnessAI.WebUI.Models;

public class TicketsViewModel
{
    public bool IsUserDataCompleted { get; set; }
    
    // Tylko dla symulacji systemu płatności
    public string NewTicketSessionId { get; set; }
}