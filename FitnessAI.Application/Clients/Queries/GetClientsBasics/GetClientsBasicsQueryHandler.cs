using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Users.Extensions;
using MediatR;

namespace FitnessAI.Application.Clients.Queries.GetClientsBasics;

public class GetClientsBasicsQueryHandler : IRequestHandler<GetClientsBasicsQuery, IEnumerable<ClientBasicsDto>>
{
    private readonly IUserRoleManagerService _userRoleManagerService;

    public GetClientsBasicsQueryHandler(
        IUserRoleManagerService userRoleManagerService)
    {
        _userRoleManagerService = userRoleManagerService;
    }

    public async Task<IEnumerable<ClientBasicsDto>> Handle(GetClientsBasicsQuery request,
        CancellationToken cancellationToken)
    {
        var clients = (await _userRoleManagerService
                .GetUsersInRoleAsync(RolesDict.KLIENT))
            .Select(x => x.ToClientBasicsDto());

        return clients;
    }
}