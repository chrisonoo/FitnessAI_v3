using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> UpdateUserExerciseSelection([FromBody] ApiCurrentUserDto currentUserDto)
    {
        currentUserDto.AccessToken = ACCESS_TOKEN;
        
        if (!await IsUserAuthorized(currentUserDto)) return Unauthorized();

        var result = new { test_string = "Workouts test text" };
        return Ok(result);
    }
}