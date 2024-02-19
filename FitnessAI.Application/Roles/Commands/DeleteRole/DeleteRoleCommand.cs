using MediatR;

namespace FitnessAI.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest
{
    public string Id { get; set; }
}