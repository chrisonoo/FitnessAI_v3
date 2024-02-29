using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class WorkoutApiController: BaseApiController
{
    public WorkoutApiController(
        SignInManager<ApplicationUser> signInManager, 
        IApplicationDbContext context, 
        IDateTimeService dateTimeService) 
        : base(signInManager, context, dateTimeService)
    {
    }
    
    [Route("{workoutId:int}")]
    public async Task<IActionResult> ExercisesForWorkout(int workoutId)
    {
        var currentWorkout = await Context.Workouts.FindAsync(workoutId);
        var currentUser = await SignInManager.UserManager.FindByIdAsync(currentWorkout!.UserId);
        var currentUserExercises = await Context.UserExercises
            .Include(ue => ue.Exercise)
            .Where(ue => ue.UserId == currentUser.Id)
            .ToListAsync();
        var workoutExercises = Context.WorkoutExercises
            .Include(we => we.Exercise)
            .Where(we => we.WorkoutId == workoutId)
            .Select(we => new ApiWorkoutExerciseDto
            {
                Id = we.Id,
                WorkoutId = we.WorkoutId,
                ExerciseId = we.ExerciseId,
                ExerciseTitle = we.Exercise.Title,
                ExerciseCategory = we.Exercise.Category
            });
        
        var currentUserExercisesNotAddedToCurrentWorkout = currentUserExercises
            .Where(ue => workoutExercises.All(we => we.ExerciseId != ue.ExerciseId))
            .Select(ue => new ApiUserExerciseDto
            {
                Id = ue.Id,
                WorkoutId = workoutId,
                ExerciseId = ue.ExerciseId,
                ExerciseTitle = ue.Exercise.Title,
                ExerciseCategory = ue.Exercise.Category
            });
        
        return Ok(new
        {
            workout_exercises = workoutExercises,
            exercises_to_add = currentUserExercisesNotAddedToCurrentWorkout
        });
    }
}