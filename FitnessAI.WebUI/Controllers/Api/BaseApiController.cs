using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers.Api;

public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator;
    protected readonly SignInManager<ApplicationUser> SignInManager;
    protected readonly IApplicationDbContext Context;
    protected readonly IDateTimeService DateTimeService;

    protected BaseApiController(
        SignInManager<ApplicationUser> signInManager, 
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
    {
        SignInManager = signInManager;
        Context = context;
        DateTimeService = dateTimeService;
    }
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

    protected async Task<bool> IsUserAuthorized(ApiCurrentUserDto curentUserDto)
    {
        const string ACCESS_TOKEN = "temporary-token-12345";
        var currentUserToken = curentUserDto.AccessToken;
        var currentUser = await SignInManager.UserManager.FindByNameAsync(curentUserDto.Username);
        
        var result = currentUser.UserName == curentUserDto.Username && currentUserToken == ACCESS_TOKEN;
        return result;
    }
}