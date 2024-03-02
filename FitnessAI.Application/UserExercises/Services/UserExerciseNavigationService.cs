using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.UserExercises.Services;

public class UserExerciseNavigationService : IUserExerciseNavigationService
{
    private static IOrderedQueryable<UserExercise> _orderedUserExercises;

    public UserExerciseNavigationService(IApplicationDbContext context)
    {
        if (!context.Exercises.Any()) throw new InvalidOperationException("Tabela ćwiczeń jest pusta");
        
        _orderedUserExercises = context.UserExercises
            .AsNoTracking()
            .OrderBy(x => x.Id);
    }

    public async Task<int> GetNextUserExerciseIdAsync(int id)
    {
        var nextExerciseId = await _orderedUserExercises
            .Where(x =>x.IsActive == true)
            .FirstOrDefaultAsync(x => x.Id > id);

        if (nextExerciseId is not null) 
            return nextExerciseId.Id;
        
        var firstExerciseId = await _orderedUserExercises.FirstAsync();
        return firstExerciseId.Id;

    }

    public async Task<int> GetPreviousUserExerciseIdAsync(int id)
    {
        var previousExerciseId = await _orderedUserExercises
            .Where(x =>x.IsActive == true)
            .LastOrDefaultAsync(q => q.Id < id);

        if (previousExerciseId is not null) 
            return previousExerciseId.Id;

        var lastExerciseId = await _orderedUserExercises.LastAsync();
        return lastExerciseId.Id;
    }
}