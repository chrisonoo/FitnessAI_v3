using System.ComponentModel.DataAnnotations;

namespace FitnessAI.Domain.Entities;

public class Exercise
{
    [Key]
    public int Id { get; set; }

    [Required]
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
    
    public ICollection<UserExercise> UserExercises { get; set; } = new HashSet<UserExercise>();
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new HashSet<WorkoutExercise>();
}