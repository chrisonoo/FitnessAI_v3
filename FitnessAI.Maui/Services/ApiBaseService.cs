namespace FitnessAI.Maui.Services;

public abstract class ApiBaseService
{
    protected static readonly HttpClient HttpClient;
    
    static ApiBaseService()
    {
        HttpClient = new HttpClient();
    }
}