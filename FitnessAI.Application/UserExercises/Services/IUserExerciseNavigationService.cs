namespace FitnessAI.Application.UserExercises.Services;

public interface IUserExerciseNavigationService
{
    Task<int> GetNextUserExerciseIdAsync(int id);
    Task<int> GetPreviousUserExerciseIdAsync(int id);
}