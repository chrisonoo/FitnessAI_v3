namespace FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;

public class WorkoutCalendarViewModel
{
    public DateTime SelectedDate { get; set; }
    public DateTime NextDate { get; set; }
    public DateTime PreviousDate { get; set; }
    // public IEnumerable<WorkoutDto> Workouts { get; set; }
    // public WorkoutDto SelectedWorkout { get; set; }
    // potrzebuję ćwiczeń, które są przypisane do treningu razem z informacją o zaawansowaniu użytkownika
    // poziom ćwiczeń można ustalić po wypełnieniu ankiety i odpytaniu sztucznej inteligencji
}