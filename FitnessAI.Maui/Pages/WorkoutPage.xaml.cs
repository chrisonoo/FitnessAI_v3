using FitnessAI.Maui.Models;
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
        var today = DateTime.Now.ToString("dd-MM-yyyy");
        var workouts = await ApiWorkoutService.Workouts(today);

        ShowDayWorkout(workouts);
    }

    private async void BtnSelectNewDay_OnClicked(object? sender, EventArgs e)
    {
        var date = ((Button)sender!).Text;
        var workouts = await ApiWorkoutService.Workouts(date);

        ShowDayWorkout(workouts);
    }

    private void ShowDayWorkout(ApiWorkoutsViewModel workouts)
    {
        PreviousDateButton.Text = workouts.PreviousDate.ToString("dd-MM-yyyy");
        SelectedDateLabel.Text = workouts.SelectedDate.ToString("dd-MM-yyyy");
        NextDateButton.Text = workouts.NextDate.ToString("dd-MM-yyyy");
        WorkoutCalendarTitleLabel.Text = workouts.WorkoutCalendarTitle ?? "Brak treningu w wybranym dniu";
        WorkoutCalendarExercisesCollectionView.ItemsSource = workouts.WorkoutCalendarExercises;
        WorkoutsList.ItemsSource = workouts.Workouts;
        AddTodayWorkoutButton.IsVisible = workouts.WorkoutCalendarId == null;
        DeleteTodayWorkoutButton.IsVisible = workouts.WorkoutCalendarId != null;
        WorkoutCalendarExercisesCollectionView.IsVisible = workouts.WorkoutCalendarId != null;
        WorkoutCalendarSelectNewWorkout.IsVisible = workouts.WorkoutCalendarId == null;
        Preferences.Set("workout_calendar_id", workouts.WorkoutCalendarId ?? 0);
        Preferences.Set("workout_selected_date", workouts.SelectedDate.ToString("dd-MM-yyyy"));
    }

    private async void DeleteTodayWorkoutButton_OnClicked(object? sender, EventArgs e)
    {
        var workoutCalendarId = Preferences.Get("workout_calendar_id", 0);
        if (workoutCalendarId == 0) return;
        
        var workout = await ApiWorkoutService.DeleteWorkout();
        ShowDayWorkout(workout);
    }

    private async void AddTodayWorkoutButton_OnClicked(object? sender, EventArgs e)
    {
        var button = (Button)sender!;
        var workoutCalendarId = (int)button.CommandParameter;
       
        var workout = await ApiWorkoutService.AddWorkout(workoutCalendarId);
        ShowDayWorkout(workout);
    }
}