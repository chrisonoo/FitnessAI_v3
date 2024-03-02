using FitnessAI.Application.UserExercises.Queries.GetUserExercises;
using FitnessAI.Domain.Entities;

namespace FitnessAI.Application.UserExercises.Extensions;

public static class UserExerciseExtensions
{
    public static UserExerciseDto ToUserExerciseDto(this UserExercise userExercise)
    {
        if (userExercise == null)
            return null;

        return new UserExerciseDto
        {
            Id = userExercise.Id,
            Title = userExercise.Title,
            Category = userExercise.Category,
            Description = userExercise.Description,
            WorkoutInstruction = userExercise.WorkoutInstruction,
            BeginnerLoad = userExercise.BeginnerLoad,
            IntermediateLoad = userExercise.IntermediateLoad,
            AdvancedLoad = userExercise.AdvancedLoad,
            ImageUrl = userExercise.ImageUrl,
            MultimediaUrl = userExercise.MultimediaUrl
        };
    }
}