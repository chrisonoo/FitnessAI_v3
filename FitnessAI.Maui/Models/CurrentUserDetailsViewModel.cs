using Newtonsoft.Json;

namespace FitnessAI.Maui.Models;

public class CurrentUserDetailsViewModel
{
    [JsonProperty("first_name")]
    public string FirstName { get; set; } = default!;
    
    [JsonProperty("last_name")]
    public string LastName { get; set; } = default!;
    
    [JsonProperty("email")]
    public string Email { get; set; } = default!;
    
    [JsonProperty("country")]
    public string Country { get; set; } = default!;
    
    [JsonProperty("zip_code")]
    public string ZipCode { get; set; } = default!;
    
    [JsonProperty("city")]
    public string City { get; set; } = default!;
    
    [JsonProperty("street")]
    public string Street { get; set; } = default!;
    
    [JsonProperty("street_number")]
    public string StreetNumber { get; set; } = default!;
    
    [JsonProperty("register_date")]
    public string RegisterDate { get; set; } = default!;
    
    [JsonProperty("account_type")]
    public string AccountType { get; set; } = default!;
}