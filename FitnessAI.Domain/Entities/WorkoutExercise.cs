using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessAI.Domain.Entities;

public class WorkoutExercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Workout))]
    public int WorkoutId { get; set; }

    public Workout Workout { get; set; }

    [Required]
    [ForeignKey(nameof(UserExercise))]
    public int UserExerciseId { get; set; }

    public UserExercise UserExercise { get; set; }
}