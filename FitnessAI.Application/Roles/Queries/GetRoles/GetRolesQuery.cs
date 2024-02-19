using MediatR;

namespace FitnessAI.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<IEnumerable<RoleDto>>
{
}