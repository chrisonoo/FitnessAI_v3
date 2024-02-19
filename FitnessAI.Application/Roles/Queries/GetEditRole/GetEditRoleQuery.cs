using FitnessAI.Application.Roles.Commands.EditRole;
using MediatR;

namespace FitnessAI.Application.Roles.Queries.GetEditRole;

public class GetEditRoleQuery : IRequest<EditRoleCommand>
{
    public string Id { get; set; }
}