using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class ApiClientActivePrintTicketViewModel
{
    [JsonProperty("company_contact_email")]
    public string CompanyContactEmail { get; set; } = default!;
    
    [JsonProperty("company_contact_phone")]
    public string CompanyContactPhone { get; set; } = default!;
    
    [JsonProperty("end_date")]
    public DateTime EndDate { get; set; }
    
    [JsonProperty("full_name")]
    public string FullName { get; set; } = default!;
    
    [JsonProperty("image_url")]
    public string ImageUrl { get; set; } = default!;
    
    [JsonProperty("start_date")]
    public DateTime StartDate { get; set; }
    
    [JsonProperty("qr_code")]
    public string QrCode { get; set; } = default!;
    
    public string FullImageUrl => $"{AppSettings.ApiUrl}/{ImageUrl}";
    public string FullEmail => $"Email: {CompanyContactEmail}";
    public string FullPhone => $"Telefon: {CompanyContactPhone}";
}