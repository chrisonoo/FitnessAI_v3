using FitnessAI.Application.Common.Interfaces;
using MediatR;

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
        await Task.CompletedTask;
        var result = new List<ExerciseDto>();
        return result;
    }
}