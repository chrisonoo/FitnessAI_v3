using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Settings.Commands.EditSettings;
using FitnessAI.Application.Settings.Queries.GetSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = $"{RolesDict.ADMINISTRATOR}")]
public class SettingsController : BaseController
{
    public async Task<IActionResult> Settings()
    {
        return View(await Mediator.Send(new GetSettingsQuery()));
    }
    
    public async Task<IActionResult> Test()
    {
        await Task.CompletedTask;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSettings(IList<SettingsDto> model)
    {
        var command = new EditSettingsCommand();
        model.ToList().ForEach(x => command.Positions.AddRange(x.Positions));

        await Mediator.Send(command);

        TempData["Success"] = "Ustawienia zostały zaktualizowane.";

        return RedirectToAction("Settings");
    }
}