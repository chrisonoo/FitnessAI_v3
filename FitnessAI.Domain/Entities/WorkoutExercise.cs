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
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }
}