using FitnessAI.Application.Exercises.Queries.GetExercises;
using FitnessAI.Domain.Entities;

namespace FitnessAI.Application.Exercises.Extensions;

public static class ExerciseExtensions
{
    public static ExerciseDto ToExerciseDto(this Exercise exercise)
    {
        if (exercise == null)
            return null;
        
        return new ExerciseDto
        {
            Id = exercise.Id,
            Title = exercise.Title,
            Category = exercise.Category,
            Description = exercise.Description,
            WorkoutInstruction = exercise.WorkoutInstruction,
            BeginnerLoad = exercise.BeginnerLoad,
            IntermediateLoad = exercise.IntermediateLoad,
            AdvancedLoad = exercise.AdvancedLoad,
            ImageUrl = exercise.ImageUrl,
            MultimediaUrl = exercise.MultimediaUrl
        };
    }
}