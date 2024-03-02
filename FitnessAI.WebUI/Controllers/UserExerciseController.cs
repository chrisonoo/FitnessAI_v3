using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.UserExercises.Queries.GetUserExerciseDetails;
using FitnessAI.Application.UserExercises.Queries.GetUserExercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class UserExerciseController : BaseController
{
    public async Task<IActionResult> UserExercises()
    {
        var result = await Mediator.Send(new GetUserExercisesQuery
        {
            ActiveUserId = LoggedUserId
        });

        return View(result);
    }
    
    public async Task<IActionResult> UserExerciseDetails(string id)
    {
        var exerciseDetailsViewModel = await Mediator.Send(new GetUserExerciseDetailsQuery
        {
            UserExerciseId = int.Parse(id),
            ActiveUserId = LoggedUserId
        });

        return View(exerciseDetailsViewModel);
    }
}