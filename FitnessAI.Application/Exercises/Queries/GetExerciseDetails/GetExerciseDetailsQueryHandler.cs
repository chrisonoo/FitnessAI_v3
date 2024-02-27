using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Exercises.Extensions;
using FitnessAI.Application.Exercises.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Exercises.Queries.GetExerciseDetails;

public class GetExerciseDetailsQueryHandler : IRequestHandler<GetExerciseDetailsQuery, ExerciseDetailsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IExerciseNavigationService _exerciseNavigationService;

    public GetExerciseDetailsQueryHandler(IApplicationDbContext context, IExerciseNavigationService exerciseNavigationService)
    {
        _context = context;
        _exerciseNavigationService = exerciseNavigationService;
    }

    public async Task<ExerciseDetailsViewModel> Handle(GetExerciseDetailsQuery request, CancellationToken cancellationToken)
    {
        var exercise = (await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken))
            .ToExerciseDto();
        
        var exerciseDetailsViewModel = new ExerciseDetailsViewModel
        {
            Exercise = exercise,
            NextExerciseId = await _exerciseNavigationService.GetNextExerciseIdAsync(request.ExerciseId),
            PreviousExerciseId = await _exerciseNavigationService.GetPreviousExerciseIdAsync(request.ExerciseId)
        };

        return exerciseDetailsViewModel;
    }
}