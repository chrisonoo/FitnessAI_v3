using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Exercises.Queries.GetExerciseDetails;
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
    
    public async Task<IActionResult> ExerciseDetails(string id)
    {
        var exercise = await Mediator.Send(new GetExerciseDetailsQuery
        {
            ExerciseId = int.Parse(id)
        });

        return View(exercise);
    }
}