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
    
    [HttpPost]
    public async Task<IActionResult> ExercisesForWorkout([FromBody] ApiWorkout workout)
    {
        var currentWorkout = await Context.Workouts.FindAsync(workout.WorkoutId);
        var currentUser = await SignInManager.UserManager.FindByIdAsync(currentWorkout!.UserId);
        var currentUserExercises = await Context.UserExercises
            .Where(ue => ue.UserId == currentUser.Id)
            .ToListAsync();
        
        return Ok(currentUserExercises);
    }
}