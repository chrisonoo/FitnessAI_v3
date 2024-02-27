using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Exercises.Services;

public class ExerciseNavigationService : IExerciseNavigationService
{
    private static IOrderedQueryable<Exercise> _orderedExercises;

    public ExerciseNavigationService(IApplicationDbContext context)
    {
        if (!context.Exercises.Any()) throw new InvalidOperationException("Tabela ćwiczeń jest pusta");
        
        _orderedExercises = context.Exercises
            .AsNoTracking()
            .OrderBy(x => x.Id);
    }

    public async Task<int> GetNextExerciseIdAsync(int id)
    {
        var nextExerciseId = await _orderedExercises
            .FirstOrDefaultAsync(x => x.Id > id);

        if (nextExerciseId is not null) 
            return nextExerciseId.Id;
        
        var firstExerciseId = await _orderedExercises.FirstAsync();
        return firstExerciseId.Id;

    }

    public async Task<int> GetPreviousExerciseIdAsync(int id)
    {
        var previousExerciseId = await _orderedExercises
            .LastOrDefaultAsync(q => q.Id < id);

        if (previousExerciseId is not null) 
            return previousExerciseId.Id;

        var lastExerciseId = await _orderedExercises.LastAsync();
        return lastExerciseId.Id;
    }
}