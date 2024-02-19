using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Components;

public class MainCarouselViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string priority)
    {
        //logika
        //pobrac

        await Task.CompletedTask;

        return View();
    }
}