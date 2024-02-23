using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class LoginPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnSignIn_Clicked(object? sender, EventArgs e)
    {
        var response = await ApiService.Login(UsernameEntry.Text, PasswordEntry.Text);
        if (response)
            Application.Current!.MainPage = new AppShell();
        else
            await DisplayAlert("Błąd", "Błędny email lub hasło", "Ok");
    }
}