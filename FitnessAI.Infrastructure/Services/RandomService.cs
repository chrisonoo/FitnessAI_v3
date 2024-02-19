using FitnessAI.Application.Common.Interfaces;

namespace FitnessAI.Infrastructure.Services;

public class RandomService : IRandomService
{
    private readonly Random _random = new();

    public string GetColor()
    {
        return string.Format("#{0:X6}", _random.Next(0x1000000));
    }
}