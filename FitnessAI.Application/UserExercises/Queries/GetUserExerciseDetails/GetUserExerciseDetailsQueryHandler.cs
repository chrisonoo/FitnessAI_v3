using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.UserExercises.Extensions;
using FitnessAI.Application.UserExercises.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.UserExercises.Queries.GetUserExerciseDetails;

public class GetUserExerciseDetailsQueryHandler : IRequestHandler<GetUserExerciseDetailsQuery, UserExerciseDetailsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserExerciseNavigationService _userExerciseNavigationService;

    public GetUserExerciseDetailsQueryHandler(IApplicationDbContext context, IUserExerciseNavigationService userExerciseNavigationService)
    {
        _context = context;
        _userExerciseNavigationService = userExerciseNavigationService;
    }

    public async Task<UserExerciseDetailsViewModel> Handle(GetUserExerciseDetailsQuery request, CancellationToken cancellationToken)
    {
        var userExercise = (await _context.UserExercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.UserExerciseId, cancellationToken))
            .ToUserExerciseDto();
        
        var exerciseDetailsViewModel = new UserExerciseDetailsViewModel
        {
            UserExercise = userExercise,
            NextUserExerciseId = await _userExerciseNavigationService.GetNextUserExerciseIdAsync(request.UserExerciseId),
            PreviousUserExerciseId = await _userExerciseNavigationService.GetPreviousUserExerciseIdAsync(request.UserExerciseId),
        };

        return exerciseDetailsViewModel;
    }
}