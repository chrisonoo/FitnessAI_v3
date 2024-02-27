namespace FitnessAI.Application.Exercises.Services;

public interface IExerciseNavigationService
{
    Task<int> GetNextExerciseIdAsync(int id);
    Task<int> GetPreviousExerciseIdAsync(int id);
}