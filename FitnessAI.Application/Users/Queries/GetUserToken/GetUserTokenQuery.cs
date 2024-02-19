using FitnessAI.Application.Common.Models.Inovices;
using MediatR;

namespace FitnessAI.Application.Users.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<AuthenticateResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}