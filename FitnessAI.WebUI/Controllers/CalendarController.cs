using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.EmployeeEvents.Commands.AddEmployeeEvent;
using FitnessAI.Application.EmployeeEvents.Commands.DeleteEmployeeEvent;
using FitnessAI.Application.EmployeeEvents.Commands.UpdateEmployeeEvent;
using FitnessAI.Application.EmployeeEvents.Queries.GetEmployeeEvents;
using FitnessAI.Application.Employees.Queries.GetEmployeeBasics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
public class CalendarController : BaseController
{
    public async Task<IActionResult> Calendar()
    {
        return View(await Mediator.Send(new GetEmployeeBasicsQuery()));
    }

    public async Task<IActionResult> GetEmployeeEvents()
    {
        return Json(await Mediator.Send(new GetEmployeeEventsQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeEvent(
        AddEmployeeEventCommand command)
    {
        await Mediator.Send(command);

        return Json(new
        {
            success = true
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmployeeEvent(
        UpdateEmployeeEventCommand command)
    {
        await Mediator.Send(command);

        return Json(new
        {
            success = true
        });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployeeEvent(int id)
    {
        await Mediator.Send(new DeleteEmployeeEventCommand { Id = id });

        return Json(new
        {
            success = true
        });
    }
}