using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers.Api;

public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}