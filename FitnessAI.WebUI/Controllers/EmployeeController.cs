using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Employees.Commands.AddEmployee;
using FitnessAI.Application.Employees.Queries.GetEditEmployee;
using FitnessAI.Application.Employees.Queries.GetEmployeeBasics;
using FitnessAI.Application.Employees.Queries.GetEmployeePage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = RolesDict.ADMINISTRATOR)]
public class EmployeeController : BaseController
{
    private readonly IDateTimeService _dateTimeService;

    public EmployeeController(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public async Task<IActionResult> Employees()
    {
        var employees = await Mediator.Send(new GetEmployeeBasicsQuery());
        return View(employees);
    }

    public async Task<IActionResult> AddEmployee()
    {
        await Task.CompletedTask;

        return View(new AddEmployeeCommand { DateOfEmployment = _dateTimeService.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEmployee(AddEmployeeCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Pracownik został dodany.";

        return RedirectToAction("Employees");
    }

    public async Task<IActionResult> EditEmployee(string employeeId)
    {
        return View(await Mediator.Send(new GetEditEmployeeQuery { UserId = employeeId }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEmployee(EditEmployeeVm viewModel)
    {
        var result = await MediatorSendValidate(viewModel.Employee);

        if (!result.IsValid)
            return View(viewModel);

        TempData["Success"] = "Dane pracownika zostały zaktualizowane.";

        return RedirectToAction("Employees");
    }

    [Route("trener/{employeePageUrl}")]
    public async Task<IActionResult> EmployeePage(string employeePageUrl)
    {
        var page = await Mediator.Send(new GetEmployeePageQuery { Url = employeePageUrl });

        if (page == null)
            return NotFound();

        return View(page);
    }
}