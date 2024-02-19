using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

public class ErrorController : Controller
{
    [HttpGet("/Error")]
    public IActionResult Index()
    {
        return View("Error");
    }
}