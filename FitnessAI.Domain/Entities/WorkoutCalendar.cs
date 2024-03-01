using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class WorkoutCalendar
{
    public readonly ICollection<WorkoutCalendarExercise> WorkoutCalendarExercises = new HashSet<WorkoutCalendarExercise>();

    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
    public DateTime Date { get; set; }

    [Required]
    public string Title { get; set; }

    public bool IsActive { get; set; }
}