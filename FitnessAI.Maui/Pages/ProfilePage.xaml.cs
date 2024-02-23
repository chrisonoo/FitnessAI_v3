namespace FitnessAI.Maui.Pages;

public partial class ProfilePage
{
    public ProfilePage()
    {
        InitializeComponent();

        LblUsername.Text = Preferences.Get("user_name", string.Empty);
    }

    private void BtnLogout_OnClicked(object? sender, EventArgs e)
    {
        Preferences.Set("access_token", string.Empty);
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
}