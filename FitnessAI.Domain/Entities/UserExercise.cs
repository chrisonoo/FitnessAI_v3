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
}