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
            })
            .OrderBy(we => we.ExerciseCategory)
            .ThenBy(we => we.ExerciseTitle);
        
        var currentUserExercisesNotAddedToCurrentWorkout = currentUserExercises
            .Where(ue => workoutExercises.All(we => we.ExerciseId != ue.ExerciseId))
            .Select(ue => new ApiUserExerciseDto
            {
                Id = ue.Id,
                WorkoutId = workoutId,
                ExerciseId = ue.ExerciseId,
                ExerciseTitle = ue.Exercise.Title,
                ExerciseCategory = ue.Exercise.Category
            })
            .OrderBy(ue => ue.ExerciseCategory)
            .ThenBy(ue => ue.ExerciseTitle);
        
        return Ok(new
        {
            workout_exercises = workoutExercises,
            exercises_to_add = currentUserExercisesNotAddedToCurrentWorkout
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> ChangeExerciseAssigment([FromBody] ApiChangeExerciseAssigmentDto changeExerciseAssigment)
    {
        if (changeExerciseAssigment.IsAssigned)
        {
            var workoutExercise = new WorkoutExercise
            {
                WorkoutId = changeExerciseAssigment.WorkoutId,
                ExerciseId = changeExerciseAssigment.ExerciseId
            };
            await Context.WorkoutExercises.AddAsync(workoutExercise);
        }
        else
        {
            var workoutExercise = await Context.WorkoutExercises
                .FirstOrDefaultAsync(we => we.WorkoutId == changeExerciseAssigment.WorkoutId && we.ExerciseId == changeExerciseAssigment.ExerciseId);
            if (workoutExercise is not null)
                Context.WorkoutExercises.Remove(workoutExercise);
        }
        
        await Context.SaveChangesAsync();
        
        return Ok();
    }
}