using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class TicketPage
{
    public TicketPage()
    {
        InitializeComponent();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        BindingContext = await ApiTicketService.ActiveTicket();
    }

    private async void BtnShowQr_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(QrCodePage)}");
    }
}