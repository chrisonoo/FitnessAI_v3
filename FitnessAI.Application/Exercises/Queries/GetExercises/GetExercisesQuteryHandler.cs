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
        var allExercises = await _context.Exercises
            .AsNoTracking()
            .Where(x=>x.IsActive == true)
            .Select(x => new ExerciseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                WorkoutInstruction = x.WorkoutInstruction,
                BeginnerLoad = x.BeginnerLoad,
                IntermidiateLoad = x.IntermidiateLoad,
                AdvancedLoad = x.AdvancedLoad,
                ImageUrl = x.ImageUrl,
                MultimediaUrl = x.MultimediaUrl
            })
            .ToListAsync(cancellationToken: cancellationToken);
        
        return allExercises;
    }
}