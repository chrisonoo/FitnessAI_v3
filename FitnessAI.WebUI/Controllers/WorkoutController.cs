using FitnessAI.Application.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class WorkoutController : BaseController
{
    public async Task<IActionResult> Workouts()
    {
        var workouts = "Workouts test text";
        await Task.CompletedTask;
        return View((object)workouts);
    }
}