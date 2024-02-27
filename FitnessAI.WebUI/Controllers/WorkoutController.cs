using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Workouts.Queries.GetWorkouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class WorkoutController : BaseController
{
    public async Task<IActionResult> Workouts()
    {
        var result = await Mediator.Send(new GetWorkoutsQuery{UserId = LoggedUserId});
        
        return View(result);
    }
}