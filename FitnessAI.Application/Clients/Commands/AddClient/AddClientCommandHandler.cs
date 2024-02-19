﻿using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Clients.Commands.AddClient;

public class AddClientCommandHandler : IRequestHandler<AddClientCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly IUserManagerService _userManagerService;

    public AddClientCommandHandler(
        IApplicationDbContext context,
        IUserManagerService userManagerService,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _userManagerService = userManagerService;
        _dateTimeService = dateTimeService;
    }

    public async Task<Unit> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        if (request.IsPrivateAccount)
            request.NipNumber = null;

        var userId = await _userManagerService.CreateAsync(request.Email, request.Password, RolesDict.KLIENT);

        var user = await _context.Users
            .Include(x => x.Client)
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == userId);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.RegisterDateTime = _dateTimeService.Now;
        user.EmailConfirmed = true;

        user.Client = new Client();
        user.Client.IsPrivateAccount = request.IsPrivateAccount;
        user.Client.NipNumber = request.NipNumber;
        user.Client.UserId = userId;

        user.Address = new Address();
        user.Address.Country = request.Country;
        user.Address.City = request.City;
        user.Address.Street = request.Street;
        user.Address.ZipCode = request.ZipCode;
        user.Address.StreetNumber = request.StreetNumber;
        user.Address.UserId = userId;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}