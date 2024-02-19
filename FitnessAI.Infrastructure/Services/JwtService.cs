using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Common.Models.Inovices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FitnessAI.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeService _dateTimeService;

    public JwtService(IConfiguration configuration,
        IDateTimeService dateTimeService)
    {
        _configuration = configuration;
        _dateTimeService = dateTimeService;
    }

    public AuthenticateResponse GenerateJwtToken(string userId)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(key);

        var token = new JwtSecurityToken(
            expires: _dateTimeService.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new AuthenticateResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}