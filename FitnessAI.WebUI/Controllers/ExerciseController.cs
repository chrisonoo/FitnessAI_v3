using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Exercises.Queries.GetExercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class ExerciseController : BaseController
{
    public async Task<IActionResult> Exercises()
    {
        var result = await Mediator.Send(new GetExercisesQuery());

        return View(result);
    }
}