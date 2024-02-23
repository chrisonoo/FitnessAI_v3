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
        
        if (response == 403)
            await DisplayAlert("Brak dostępu", "Uzupełnij swój profil w aplikacji webowej", "Ok");
        else if (response == 401)
            await DisplayAlert("Błąd", "Błędny email lub hasło", "Ok");
        else if (response != 200)
            await DisplayAlert("Nieoczekiwany błąd", "Brak dostępu, zgłoś się do administratora", "Ok");
        else
            Application.Current!.MainPage = new AppShell();
    }
}