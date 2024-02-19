using AspNetCore.ReCaptcha;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Contacts.Commands.SendContactEmail;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

public class HomeController : BaseController
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger _logger;

    public HomeController(
        ILogger<HomeController> logger,
        IDateTimeService dateTimeService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
    }

    public async Task<IActionResult> Index()
    {
        await Task.CompletedTask;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View(new SendContactEmailCommand());
    }

    [ValidateReCaptcha]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Contact(SendContactEmailCommand command)
    {
        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
        {
            ModelState.AddModelError("AntySpamResult", "Wypełnij pole ReCaptcha (zabezpieczenie przez spamem)");
            return View(command);
        }

        TempData["Success"] = "Wiadomość została wysłana do administratora.";

        return RedirectToAction("Contact");
    }

    [HttpPost]
    public IActionResult SetCulture(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = _dateTimeService.Now.AddYears(1) });

        return LocalRedirect(returnUrl);
    }
}