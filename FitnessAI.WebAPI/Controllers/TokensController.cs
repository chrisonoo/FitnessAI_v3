using FitnessAI.Application.Users.Queries.GetUserToken;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebAPI.Controllers;

[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
public class TokensController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Generate(GetUserTokenQuery command)
    {
        var token = await Mediator.Send(command);

        if (token == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(token);
    }
}