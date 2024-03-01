using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class WorkoutCalendarExercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(WorkoutCalendar))]
    public int WorkoutCalendarId { get; set; }

    public WorkoutCalendar WorkoutCalendar { get; set; }

    [Required]
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; }

    public Exercise Exercise { get; set; }

    [Required]
    public string WorkoutLoad { get; set; }

    public bool IsActive { get; set; }
}