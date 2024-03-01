using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Exercises.Queries.GetExercises;

public class GetExercisesQuteryHandler: IRequestHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetExercisesQuteryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        var userExercisesForActiveUser = await _context.UserExercises
            .Where(ue => ue.UserId == request.ActiveUserId)
            .Where(ue => ue.IsActive)
            .Select(ue => ue.ExerciseId)
            .ToListAsync(cancellationToken);

        var allExercises = await _context.Exercises
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => new ExerciseDto
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
                IsSelectedForActiveUser = userExercisesForActiveUser.Contains(x.Id)
            })
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
        
        return allExercises;
    }
}