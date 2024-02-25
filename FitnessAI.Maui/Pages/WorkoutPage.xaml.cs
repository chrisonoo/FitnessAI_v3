using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class WorkoutPage
{
    public WorkoutPage()
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
        BindingContext = await ApiWorkoutService.Workouts();
    }
}