using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Settings.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Settings.Queries.GetSettings;

public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, IList<SettingsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSettingsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<SettingsDto>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = await _context.Settings
            .AsNoTracking()
            .Include(x => x.Positions.OrderBy(y => y.Order))
            .OrderBy(x => x.Order)
            .Select(x => x.ToDto())
            .ToListAsync();

        return settings;
    }
}