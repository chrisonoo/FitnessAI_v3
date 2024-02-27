using FitnessAI.Application.Clients.Commands.AddClient;
using FitnessAI.Application.Clients.Commands.EditClient;
using FitnessAI.Application.Clients.Queries.GetClient;
using FitnessAI.Application.Clients.Queries.GetClientDashboard;
using FitnessAI.Application.Clients.Queries.GetClientsBasics;
using FitnessAI.Application.Clients.Queries.GetEditAdminClient;
using FitnessAI.Application.Clients.Queries.GetEditClient;
using FitnessAI.Application.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize]
public class ClientController : BaseController
{
    public async Task<IActionResult> Dashboard()
    {
        var vm = await Mediator.Send(new GetClientDashboardQuery { UserId = LoggedUserId });
        return View(vm);
    }

    public async Task<IActionResult> Client()
    {
        return View(await Mediator.Send(new GetClientQuery { UserId = LoggedUserId }));
    }

    public async Task<IActionResult> EditClient()
    {
        return View(await Mediator.Send(new GetEditClientQuery { UserId = LoggedUserId }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditClient(EditClientCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Twoje dane zostały zaktualizowane";

        return RedirectToAction("Client");
    }

    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> Clients()
    {
        return View(await Mediator.Send(new GetClientsBasicsQuery()));
    }

    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> AddClient()
    {
        await Task.CompletedTask;

        return View(new AddClientCommand());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> AddClient(AddClientCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Dane o klientach zostały zaktualizowane";

        return RedirectToAction("Clients");
    }

    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> EditAdminClient(string clientId)
    {
        return View(await Mediator.Send(new GetEditAdminClientQuery { UserId = clientId }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{RolesDict.ADMINISTRATOR},{RolesDict.PRACOWNIK}")]
    public async Task<IActionResult> EditAdminClient(EditAdminClientVm vm)
    {
        var result = await MediatorSendValidate(vm.Client);

        if (!result.IsValid)
            return View(vm);

        TempData["Success"] = "Dane o klientach zostały zaktualizowane";

        return RedirectToAction("Clients");
    }
}