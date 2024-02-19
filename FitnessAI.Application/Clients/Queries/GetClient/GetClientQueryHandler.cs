﻿using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Users.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Clients.Queries.GetClient;

public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientDto>
{
    private readonly IApplicationDbContext _context;

    public GetClientQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClientDto> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Include(x => x.Client)
            .Include(x => x.Address)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.UserId);

        return user.ToClientDto();
    }
}