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
    public async Task<IActionResult> Login([FromBody] ApiLoginModel model)
    {
        const string ACCESS_TOKEN = "temporary-token-12345";
        
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded) return Unauthorized();
        
        var currentUser = await _signInManager.UserManager.FindByNameAsync(model.Username);
            
        return new ObjectResult(new
        {
            access_token = ACCESS_TOKEN,
            user_id = currentUser.Id,
            user_name = currentUser.UserName
        });
    }
}