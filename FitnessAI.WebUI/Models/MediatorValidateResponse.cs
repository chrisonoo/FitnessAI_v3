namespace FitnessAI.WebUI.Models;

public class MediatorValidateResponse<T>
{
    public bool IsValid { get; set; }
    public T Model { get; set; }
}