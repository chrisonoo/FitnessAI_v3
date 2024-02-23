using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ApiUsersController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ApiUsersController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] ApiUserLoginDto userLoginDto)
    {
        const string ACCESS_TOKEN = "temporary-token-12345";

        var result = await _signInManager.PasswordSignInAsync(
            userLoginDto.Username ?? string.Empty,
            userLoginDto.Password ?? string.Empty,
            false,
            false);
        if (!result.Succeeded) return Unauthorized();
        
        var currentUser = await _signInManager.UserManager.FindByNameAsync(userLoginDto.Username);
        if (currentUser.FirstName == null || currentUser.LastName == null)
            return StatusCode(403);

        return Ok(new
        {
            access_token = ACCESS_TOKEN,
            user_id = currentUser.Id,
            user_name = currentUser.UserName
        });
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> CurrentUserDetails([FromBody] ApiCurrentUserDto curentUserDto)
    {
        if (await IsUserAuthorized(curentUserDto)) return Unauthorized();
        
        var currentUser = await _signInManager.UserManager.FindByNameAsync(curentUserDto.Username);
        
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
            account_type = currentUser.Client.IsPrivateAccount ? "Prywatne" : "Firmowe"
        });
    }
    
    private async Task<bool> IsUserAuthorized(ApiCurrentUserDto curentUserDto)
    {
        const string ACCESS_TOKEN = "temporary-token-12345";
        var currentUserToken = curentUserDto.AccessToken;
        var currentUser = await _signInManager.UserManager.FindByNameAsync(curentUserDto.Username);
        
        return currentUser != null && currentUserToken == ACCESS_TOKEN;
    }
}