using FitnessAI.Application.Common.Models.Inovices;

namespace FitnessAI.Application.Common.Interfaces;

public interface IJwtService
{
    AuthenticateResponse GenerateJwtToken(string userId);
}