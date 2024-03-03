using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class  WorkoutCalendarExercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(WorkoutCalendar))]
    public int WorkoutCalendarId { get; set; }

    public WorkoutCalendar WorkoutCalendar { get; set; }

    [Required]
    [ForeignKey(nameof(UserExercise))]
    public int UserExerciseId { get; set; }

    public UserExercise UserExercise { get; set; }

    [Required]
    public string WorkoutLoad { get; set; }

    public bool IsActive { get; set; }
}