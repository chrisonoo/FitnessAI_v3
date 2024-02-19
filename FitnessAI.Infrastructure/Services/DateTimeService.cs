using FitnessAI.Application.Common.Interfaces;

namespace FitnessAI.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}