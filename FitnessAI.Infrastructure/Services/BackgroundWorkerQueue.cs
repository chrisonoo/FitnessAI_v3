using System.Collections.Concurrent;
using FitnessAI.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessAI.Infrastructure.Services;

public class BackgroundWorkerQueue : IBackgroundWorkerQueue
{
    private readonly ILogger<BackgroundWorkerQueue> _logger;
    private readonly SemaphoreSlim _semaphore = new(0);
    private readonly ConcurrentQueue<Func<CancellationToken, Task>> _workItems = new();

    public BackgroundWorkerQueue(ILogger<BackgroundWorkerQueue> logger)
    {
        _logger = logger;
    }

    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);

        _workItems.TryDequeue(out var workItem);

        return workItem;
    }

    public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem, string workItemDescription)
    {
        _logger.LogInformation($"QueueBackgroundWorkItem Start... {workItemDescription}");

        if (workItem == null)
            throw new ArgumentNullException(nameof(workItem));

        _workItems.Enqueue(workItem);

        _semaphore.Release();
    }
}