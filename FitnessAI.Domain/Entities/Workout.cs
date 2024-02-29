using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class Workout
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
}