using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize]
public class UserController : BaseController
{
    private readonly ILogger _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            await Mediator.Send(
                new DeleteUserCommand
                {
                    Id = id
                });

            return Json(new { success = true });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }
}