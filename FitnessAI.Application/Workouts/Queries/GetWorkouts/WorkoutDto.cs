using FitnessAI.Application.Exercises.Queries.GetExercises;
using FitnessAI.Domain.Enums;

namespace FitnessAI.Application.Workouts.Queries.GetWorkouts;

public class WorkoutDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<WeekDaysEnum> WorkoutDays { get; set; }
    public List<ExerciseDto> Excercises { get; set; }
    public string UserId { get; set; }
}