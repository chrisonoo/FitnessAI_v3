namespace FitnessAI.Application.Common.Events;

public interface IEventDispatcher
{
    Task PublishAsync<T>(T @event) where T : class, IEvent;
}