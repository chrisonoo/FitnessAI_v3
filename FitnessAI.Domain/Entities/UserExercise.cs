using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class UserExercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    [Required]
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; }

    public Exercise Exercise { get; set; }
    
    public string Title { get; set; }

    [Required]
    public string Category { get; set; }

    public string Description { get; set; }
    public string WorkoutInstruction { get; set; }
    public string BeginnerLoad { get; set; }
    public string IntermediateLoad { get; set; }
    public string AdvancedLoad { get; set; }
    public string ImageUrl { get; set; }
    public string MultimediaUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new HashSet<WorkoutExercise>();
    public ICollection<WorkoutCalendarExercise> WorkoutCalendarExercises { get; set; } = new HashSet<WorkoutCalendarExercise>();
}