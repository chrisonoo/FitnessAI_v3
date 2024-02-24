using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        LblUserName.Text
            = "Cześć, "
              + Preferences.Get("first_name", string.Empty) + " "
              + Preferences.Get("last_name", string.Empty) + "!";
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        var homeData = await ApiHomeService.Home();
        BindingContext = homeData;
        
        var expiryDate = homeData.TicketEndDate;
        var remainingDays = expiryDate - DateTime.Now;
        LblTicketDaysLeft.Text = $"({remainingDays.Days} dni)";
    }
}