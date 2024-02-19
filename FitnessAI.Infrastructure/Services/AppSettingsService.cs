using FitnessAI.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Infrastructure.Services;

public class AppSettingsService : IAppSettingsService
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private readonly Dictionary<string, string> _appSettings = new();

    public async Task<string> Get(string key)
    {
        try
        {
            await Wait();
            return _appSettings[key];
        }
        finally
        {
            Release();
        }
    }

    public async Task Update(IApplicationDbContext context)
    {
        try
        {
            await Wait();

            _appSettings.Clear();

            var settingsPositions = await context
                .SettingsPositions
                .AsNoTracking()
                .ToListAsync();

            foreach (var position in settingsPositions)
                _appSettings.Add(position.Key, position.Value);
        }
        finally
        {
            Release();
        }
    }

    private async Task Wait()
    {
        await Semaphore.WaitAsync();
    }

    private void Release()
    {
        Semaphore.Release();
    }
}