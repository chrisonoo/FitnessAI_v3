using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Tickets.Queries.GetPrintTicket;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApiTicketsController : BaseApiController
{
    public ApiTicketsController(
        SignInManager<ApplicationUser> signInManager,
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
        : base(signInManager, context, dateTimeService)
    {
    }

    [HttpPost]
    public async Task<IActionResult> ActiveTicket([FromBody] ApiCurrentUserDto curentUserDto)
    {
        if (!await IsUserAuthorized(curentUserDto)) return Unauthorized();

        var currentUser = await Context.Users
            .Include(u => u.Tickets)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == curentUserDto.Username);
        var clientActiveTicket = currentUser.Tickets.FirstOrDefault(x =>
            x.StartDate.Date <= DateTimeService.Now.Date &&
            x.EndDate.Date >= DateTimeService.Now.Date);
        var clientActivePrintTicket = await Mediator.Send(new GetPrintTicketQuery
        {
            TicketId = clientActiveTicket!.Id, UserId = currentUser.Id
        });


        var result = new { };
        return Ok(result);
    }
}