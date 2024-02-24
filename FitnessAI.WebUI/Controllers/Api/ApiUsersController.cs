using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApiUsersController : BaseApiController
{
    public ApiUsersController(
        SignInManager<ApplicationUser> signInManager,
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
        : base(signInManager, context, dateTimeService)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] ApiUserLoginDto userLoginDto)
    {
        const string ACCESS_TOKEN = "temporary-token-12345";

        var result = await SignInManager.PasswordSignInAsync(
            userLoginDto.Username ?? string.Empty,
            userLoginDto.Password ?? string.Empty,
            false,
            false);
        if (!result.Succeeded) return Unauthorized();
        
        var currentUser = await SignInManager.UserManager.FindByNameAsync(userLoginDto.Username);
        if (currentUser.FirstName == null || currentUser.LastName == null)
            return StatusCode(403);

        return Ok(new
        {
            access_token = ACCESS_TOKEN,
            user_id = currentUser.Id,
            user_name = currentUser.UserName,
            first_name = currentUser.FirstName,
            last_name = currentUser.LastName
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> CurrentUserDetails([FromBody] ApiCurrentUserDto curentUserDto)
    {
        if (!await IsUserAuthorized(curentUserDto)) return Unauthorized();
        
        var currentUser = await Context.Users
            .Include(u => u.Client)
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.UserName == curentUserDto.Username);
        
        return Ok(new
        {
            first_name = currentUser.FirstName,
            last_name = currentUser.LastName,
            email = currentUser.Email,
            country = currentUser.Address?.Country,
            zip_code = currentUser.Address?.ZipCode,
            city = currentUser.Address?.City,
            street = currentUser.Address?.Street,
            street_number = currentUser.Address?.StreetNumber,
            register_date = currentUser.RegisterDateTime.ToString("yyyy-MM-dd"),
            account_type = currentUser.Client.IsPrivateAccount ? "Konto prywatne" : "Konto firmowe"
        });
    }
}