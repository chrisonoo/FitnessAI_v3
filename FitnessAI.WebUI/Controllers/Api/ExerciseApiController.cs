using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class ExerciseApiController : BaseApiController
{
    public ExerciseApiController(
        SignInManager<ApplicationUser> signInManager, 
        IApplicationDbContext context, 
        IDateTimeService dateTimeService) 
        : base(signInManager, context, dateTimeService)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateUserExerciseSelection([FromBody] ApiUpdateUserExercisesSelection updateUserExercisesSelection)
    {
        var currentUserDto = new ApiCurrentUserDto
        {
            Username = updateUserExercisesSelection.Username,
            AccessToken = ACCESS_TOKEN
        };
        if (!await IsUserAuthorized(currentUserDto)) return Unauthorized();
        
        var currentUser = await SignInManager.UserManager.FindByNameAsync(updateUserExercisesSelection.Username);
        var userExercise = await Context.UserExercises
            .FirstOrDefaultAsync(ue => ue.ExerciseId == updateUserExercisesSelection.ExerciseId && ue.UserId == currentUser.Id);
        
        if (userExercise is null)
        {
            var newUserExercise = new UserExercise
            {
                ExerciseId = updateUserExercisesSelection.ExerciseId,
                UserId = currentUser.Id
            };
            await Context.UserExercises.AddAsync(newUserExercise);
        }
        else
        {
            Context.UserExercises.Remove(userExercise);
        }
        
        await Context.SaveChangesAsync();
        
        var result = new { test_string = "Workouts test text" };
        return Ok(result);
    }
}