using FitnessAI.Application.Clients.Commands.EditAdminClient;
using FitnessAI.Application.Roles.Queries.GetRoles;

namespace FitnessAI.Application.Clients.Queries.GetEditAdminClient;

public class EditAdminClientVm
{
    public EditAdminClientCommand Client { get; set; }
    public List<RoleDto> AvailableRoles { get; set; }
}