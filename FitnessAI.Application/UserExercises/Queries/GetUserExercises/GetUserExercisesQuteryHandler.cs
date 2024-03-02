using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.UserExercises.Queries.GetUserExercises;

public class GetUserExercisesQuteryHandler : IRequestHandler<GetUserExercisesQuery, IEnumerable<UserExerciseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUserExercisesQuteryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserExerciseDto>> Handle(GetUserExercisesQuery request,
        CancellationToken cancellationToken)
    {
        var allUserExercises = await _context.UserExercises
            .AsNoTracking()
            .Where(x => x.UserId == request.ActiveUserId)
            .Where(x => x.IsActive == true)
            .Select(x => new UserExerciseDto
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category,
                Description = x.Description,
                WorkoutInstruction = x.WorkoutInstruction,
                BeginnerLoad = x.BeginnerLoad,
                IntermediateLoad = x.IntermediateLoad,
                AdvancedLoad = x.AdvancedLoad,
                ImageUrl = x.ImageUrl,
                MultimediaUrl = x.MultimediaUrl,
                IsActive = x.IsActive
            })
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);

        return allUserExercises;
    }
}