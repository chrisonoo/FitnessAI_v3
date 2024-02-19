﻿using FitnessAI.Application.Common.Extensions;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Clients.Commands.EditAdminClient;

public class EditAdminClientCommandHandler : IRequestHandler<EditAdminClientCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IRoleManagerService _roleManagerService;
    private readonly IUserRoleManagerService _userRoleManagerService;

    public EditAdminClientCommandHandler(
        IApplicationDbContext context,
        IUserRoleManagerService userRoleManagerService,
        IRoleManagerService roleManagerService)
    {
        _context = context;
        _userRoleManagerService = userRoleManagerService;
        _roleManagerService = roleManagerService;
    }

    public async Task<Unit> Handle(EditAdminClientCommand request, CancellationToken cancellationToken)
    {
        if (request.IsPrivateAccount)
            request.NipNumber = null;

        var user = await _context.Users
            .Include(x => x.Client)
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        if (user.Client == null)
            user.Client = new Client();

        user.Client.IsPrivateAccount = request.IsPrivateAccount;
        user.Client.NipNumber = request.NipNumber;
        user.Client.UserId = request.Id;

        if (user.Address == null)
            user.Address = new Address();

        user.Address.Country = request.Country;
        user.Address.City = request.City;
        user.Address.Street = request.Street;
        user.Address.ZipCode = request.ZipCode;
        user.Address.StreetNumber = request.StreetNumber;
        user.Address.UserId = request.Id;

        await _context.SaveChangesAsync(cancellationToken);

        if (request.RoleIds != null && request.RoleIds.Any())
            await UpdateRoles(request.RoleIds, request.Id);

        return Unit.Value;
    }

    private async Task UpdateRoles(List<string> newRoleIds, string userId)
    {
        var roles = _roleManagerService.GetRoles().Select(x => new IdentityRole { Id = x.Id, Name = x.Name });

        var oldRoles = await GetOldRoles(userId);
        var newRoles = GetNewRoles(newRoleIds, roles);

        await RemoveRoles(userId, oldRoles, newRoles);

        await AddNewRoles(userId, oldRoles, newRoles);
    }

    private async Task AddNewRoles(string userId, List<IdentityRole> oldRoles, List<IdentityRole> newRoles)
    {
        var roleToAdd = newRoles.Except(oldRoles, x => x.Id);

        foreach (var role in roleToAdd)
            await _userRoleManagerService.AddToRoleAsync(userId, role.Name);
    }

    private async Task RemoveRoles(string userId, List<IdentityRole> oldRoles, List<IdentityRole> newRoles)
    {
        var roleToRemove = oldRoles.Except(newRoles, x => x.Id);

        foreach (var role in roleToRemove)
            await _userRoleManagerService.RemoveFromRoleAsync(userId, role.Name);
    }

    private List<IdentityRole> GetNewRoles(List<string> newRoleIds, IEnumerable<IdentityRole> roles)
    {
        var newRoles = new List<IdentityRole>();

        var roleDict = roles.ToDictionary(role => role.Id, role => role);

        foreach (var roleId in newRoleIds)
            newRoles.Add(new IdentityRole { Id = roleId, Name = roleDict[roleId].Name });

        return newRoles;
    }

    private async Task<List<IdentityRole>> GetOldRoles(string userId)
    {
        return (await _userRoleManagerService.GetRolesAsync(userId)).ToList();
    }
}