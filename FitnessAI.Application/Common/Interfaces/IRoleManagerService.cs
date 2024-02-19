using FitnessAI.Application.Roles.Queries.GetRoles;

namespace FitnessAI.Application.Common.Interfaces;

public interface IRoleManagerService
{
    IEnumerable<RoleDto> GetRoles();
    Task CreateAsync(string roleName);
    Task UpdateAsync(RoleDto role);
    Task<RoleDto> FindByIdAsync(string id);
    Task DeleteAsync(string id);
}