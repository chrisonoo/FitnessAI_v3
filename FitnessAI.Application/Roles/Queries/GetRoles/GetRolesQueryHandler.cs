using FitnessAI.Application.Common.Interfaces;
using MediatR;

namespace FitnessAI.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IRoleManagerService _roleManagerService;

    public GetRolesQueryHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _roleManagerService.GetRoles();
    }
}