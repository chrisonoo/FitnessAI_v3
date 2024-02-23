using FitnessAI.Maui.Pages;

namespace FitnessAI.Maui;

public partial class App
{
    public App()
    {
        InitializeComponent();

        // Wymuszenie jasnego motywu
        Current!.UserAppTheme = AppTheme.Light;
        
        var accessToken = Preferences.Get("access_token", string.Empty);

        MainPage = string.IsNullOrEmpty(accessToken) ? new NavigationPage(new LoginPage()) : new AppShell();
    }
}
