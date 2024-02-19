using FitnessAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitnessAI.Application.Common.Interfaces;

public interface IUserRoleManagerService
{
    Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);
    Task<IEnumerable<IdentityRole>> GetRolesAsync(string userId);
    Task AddToRoleAsync(string userId, string roleName);
    Task RemoveFromRoleAsync(string userId, string roleName);
}