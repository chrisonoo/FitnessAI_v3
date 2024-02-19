using FitnessAI.Application.Clients.Commands.EditAdminClient;
using FitnessAI.Application.Clients.Commands.EditClient;
using FitnessAI.Application.Clients.Queries.GetClient;
using FitnessAI.Application.Clients.Queries.GetClientsBasics;
using FitnessAI.Application.Employees.Commands.EditEmployee;
using FitnessAI.Application.Employees.Queries.GetEmployeeBasics;
using FitnessAI.Domain.Entities;
using FitnessAI.Domain.Enums;

namespace FitnessAI.Application.Users.Extensions;

public static class UserExtensions
{
    public static ClientDto ToClientDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new ClientDto
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true,
            RegisterDateTime = user.RegisterDateTime
        };
    }

    public static EditClientCommand ToEditClientCommand(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditClientCommand
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true
        };
    }

    public static ClientBasicsDto ToClientBasicsDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new ClientBasicsDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = !string.IsNullOrWhiteSpace(user.FirstName) ||
                   !string.IsNullOrWhiteSpace(user.LastName)
                ? $"{user.FirstName} {user.LastName}"
                : "-",
            IsDeleted = user.IsDeleted
        };
    }

    public static EditAdminClientCommand ToEditAdminClientCommand(
        this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditAdminClientCommand
        {
            Id = user.Id,
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NipNumber = user.Client?.NipNumber,
            IsPrivateAccount = user.Client?.IsPrivateAccount ?? true
        };
    }

    public static EmployeeBasicsDto ToEmployeeBasicsDto(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EmployeeBasicsDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = !string.IsNullOrWhiteSpace(user.FirstName) ||
                   !string.IsNullOrWhiteSpace(user.LastName)
                ? $"{user.FirstName} {user.LastName}"
                : "-",
            IsDeleted = user.IsDeleted
        };
    }

    public static EditEmployeeCommand ToEmployee(this ApplicationUser user)
    {
        if (user == null)
            return null;

        return new EditEmployeeCommand
        {
            City = user.Address?.City,
            Country = user.Address?.Country,
            Street = user.Address?.Street,
            StreetNumber = user.Address?.StreetNumber,
            ZipCode = user.Address?.ZipCode,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            DateOfDismissal = user.Employee?.DateOfDismissal,
            DateOfEmployment = user.Employee?.DateOfEmployment ?? DateTime.MinValue,
            ImageUrl = user.Employee?.ImageUrl,
            PositionId = (int?)user.Employee?.Position ?? (int)Position.Receptionits,
            Salary = user.Employee?.Salary ?? 0,
            WebsiteRaw = user.Employee?.WebsiteRaw,
            WebsiteUrl = user.Employee?.WebsiteUrl
        };
    }
}