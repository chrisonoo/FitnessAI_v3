using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class WorkoutCalendarController : BaseController
{
    public async Task<IActionResult> WorkoutsCalendar()
    {
        var result = await Mediator.Send(new GetWorkoutsCalendarQuery { UserId = LoggetUserId });

        return View(result);
    }
}