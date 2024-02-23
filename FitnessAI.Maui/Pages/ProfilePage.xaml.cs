using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class ProfilePage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private void BtnLogout_OnClicked(object? sender, EventArgs e)
    {
        Preferences.Set("access_token", string.Empty);
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDataAsync();
    }
    
    public async Task LoadDataAsync()
    {
        BindingContext = await ApiService.CurrentUserDetails();
    }
}