using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.WorkoutsCalendar.Command.DeleteSelectedWorkout;
using FitnessAI.Application.WorkoutsCalendar.Command.SetWorkoutAsDone;
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
    
    [HttpPost]
    public async Task<IActionResult> SetSelectedWorkoutAsDone(string workoutId, string workoutDate)
    {
        var result = await Mediator.Send(new SetWorkoutAsDoneCommand { WorkoutId = workoutId, UserId = LoggedUserId, WorkoutDate = workoutDate });
        
        return RedirectToAction("WorkoutsCalendar", "WorkoutCalendar", new { date = workoutDate });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteSelectedWorkout(string workoutCalendarId, string workoutDate)
    {
        var result = await Mediator.Send(new DeleteSelectedWorkoutCommand { WorkoutCalendarId = workoutCalendarId, UserId = LoggedUserId, WorkoutDate = workoutDate });
        
        return RedirectToAction("WorkoutsCalendar", "WorkoutCalendar", new { date = workoutDate });
    }
}