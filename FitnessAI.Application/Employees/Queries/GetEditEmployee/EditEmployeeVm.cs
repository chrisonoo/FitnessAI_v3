using FitnessAI.Application.Employees.Commands.EditEmployee;
using FitnessAI.Application.Roles.Queries.GetRoles;

namespace FitnessAI.Application.Employees.Queries.GetEditEmployee;

public class EditEmployeeVm
{
    public EditEmployeeCommand Employee { get; set; }
    public List<RoleDto> AvailableRoles { get; set; }
    public string EmployeeFullPathImage { get; set; }
}