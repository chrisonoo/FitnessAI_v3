using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, IEnumerable<WorkoutDto>>
{
    private readonly IApplicationDbContext _context;

    public GetWorkoutsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutDto>> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var allWorkouts = await _context.Workouts
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .Where(x => x.IsActive)
            .Select(x => new WorkoutDto
            {
                Id = x.Id,
                Title = x.Title,
                UserId = x.UserId
            })
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken: cancellationToken);
        

        return allWorkouts;
    }
}