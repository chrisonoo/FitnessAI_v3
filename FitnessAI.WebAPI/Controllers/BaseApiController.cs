using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

    protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
}