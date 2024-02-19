﻿using FitnessAI.Application.Clients.Commands.EditClient;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Users.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Clients.Queries.GetEditClient;

public class GetEditClientQueryHandler : IRequestHandler<GetEditClientQuery, EditClientCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditClientQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditClientCommand> Handle(GetEditClientQuery request, CancellationToken cancellationToken)
    {
        return (await _context.Users
                .Include(x => x.Client)
                .Include(x => x.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.UserId))
            .ToEditClientCommand();
    }
}