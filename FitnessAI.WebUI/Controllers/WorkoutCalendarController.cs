using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}, {RolesDict.KLIENT}")]
public class WorkoutCalendarController : BaseController
{
    [HttpGet("[controller]/[action]/{date?}")]
    public async Task<IActionResult> WorkoutsCalendar(string date = null)
    {
        var selectedDate = DateTime.Today;
        if (date != null) selectedDate = DateTime.Parse(date);

        var result =
            await Mediator.Send(new GetWorkoutsCalendarQuery { UserId = LoggedUserId, SelectedDate = selectedDate });

        return View(result);
    }
}