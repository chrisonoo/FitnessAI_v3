using FitnessAI.Application.Clients.Queries.GetClientDashboard;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApiHomeController:BaseApiController
{
    public ApiHomeController(SignInManager<ApplicationUser> signInManager, IApplicationDbContext context) : base(signInManager, context)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> Home([FromBody] ApiCurrentUserDto curentUserDto)
    {
        if (!await IsUserAuthorized(curentUserDto)) return Unauthorized();

        var currentUser = await SignInManager.UserManager.FindByNameAsync(curentUserDto.Username);
        var result = await Mediator.Send(new GetClientDashboardQuery { UserId = currentUser.Id });
        
        return Ok(result);
    }
}