namespace FitnessAI.Maui;

public static class AppSettings
{
    public static readonly string ApiUrl = 
        DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5184" : "http://localhost:5184";
}