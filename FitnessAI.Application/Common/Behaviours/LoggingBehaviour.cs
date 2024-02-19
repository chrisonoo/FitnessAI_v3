﻿using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitnessAI.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        var userName = _currentUserService.UserName ?? string.Empty;

        _logger.LogInformation($"Handling {requestName}");

        _logger.LogInformation("FitnessAI Request: {@Name} {@UserId} {@UserName} {@Request}", requestName, userId,
            userName, request);

        var response = await next();

        _logger.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}