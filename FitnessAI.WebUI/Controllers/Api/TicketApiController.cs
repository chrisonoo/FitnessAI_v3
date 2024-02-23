using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;
using FitnessAI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/ticket")]
[ApiController]
public class TicketApiController : BaseApiController
{
    public TicketApiController(SignInManager<ApplicationUser> signInManager, IApplicationDbContext context) : base(signInManager, context)
    {
    }
    
    [Route("updatestatus")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateStatus(MarkTicketAsPaidCommand command)
    {
        try
        {
            await Mediator.Send(command);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        var json = JsonConvert.SerializeObject(new { success = true });
        return new ContentResult
        {
            Content = json,
            ContentType = "application/json"
        };
    }
}