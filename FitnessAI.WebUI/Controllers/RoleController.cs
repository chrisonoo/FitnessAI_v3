using FitnessAI.Application.Common.Exceptions;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Roles.Commands.AddRole;
using FitnessAI.Application.Roles.Commands.DeleteRole;
using FitnessAI.Application.Roles.Commands.EditRole;
using FitnessAI.Application.Roles.Queries.GetEditRole;
using FitnessAI.Application.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = RolesDict.ADMINISTRATOR)]
public class RoleController : BaseController
{
    private readonly ILogger _logger;

    public RoleController(ILogger<RoleController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Roles()
    {
        return View(await Mediator.Send(new GetRolesQuery()));
    }

    public IActionResult AddRole()
    {
        return View(new AddRoleCommand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRole(AddRoleCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Role zostały zaktualizowane";

        return RedirectToAction("Roles");
    }

    public async Task<IActionResult> EditRole(string id)
    {
        return View(await Mediator.Send(new GetEditRoleQuery { Id = id }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Role zostały zaktualizowane";

        return RedirectToAction("Roles");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
        try
        {
            await Mediator.Send(new DeleteRoleCommand { Id = id });
            await Task.Delay(5000);
            return Json(new { success = true });
        }
        catch (ValidationException exception)
        {
            return Json(new
            {
                success = false,
                message = string.Join(". ", exception.Errors.Select(x => string.Join(". ", x.Value.Select(y => y))))
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }
}