using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Roles.Queries.GetRoles;
using MediatR;

namespace FitnessAI.Application.Roles.Commands.EditRole;

public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
{
    private readonly IRoleManagerService _roleManagerService;

    public EditRoleCommandHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }

    public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManagerService.UpdateAsync(new RoleDto { Id = request.Id, Name = request.Name });

        return Unit.Value;
    }
}