using FitnessAI.Application.Common.Exceptions;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace FitnessAI.Infrastructure.Services;

public class UserManagerService : IUserManagerService
{
    private readonly IUserEmailStore<ApplicationUser> _userEmailStore;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    public UserManagerService(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
        _userEmailStore = GetEmailStore();
    }

    public async Task<string> CreateAsync(string email, string password, string role)
    {
        var user = new ApplicationUser();

        await _userStore.SetUserNameAsync(user, email, CancellationToken.None);

        await _userEmailStore.SetEmailAsync(user, email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            foreach (var item in result.Errors)
                throw new ValidationException(new List<ValidationFailure>
                {
                    new(item.Code, item.Description)
                });

        if (!string.IsNullOrWhiteSpace(role))
            await _userManager.AddToRoleAsync(user, role);

        return await _userManager.GetUserIdAsync(user);
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email supprt");

        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
}