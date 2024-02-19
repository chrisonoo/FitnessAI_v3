﻿using FitnessAI.Application.Common.Interfaces;
using FluentValidation;

namespace FitnessAI.Application.Tickets.Commands.MarkTicketAsPaidCommand;

public class MarkTicketAsPaidCommandValidator : AbstractValidator<MarkTicketAsPaidCommand>
{
    private readonly IHttpContext _httpContext;

    public MarkTicketAsPaidCommandValidator(IHttpContext httpContext)
    {
        _httpContext = httpContext;

        RuleFor(x => x.IsProduction)
            .Must(SendFromValidIpAddress)
            .WithMessage("Przelew24 – not allowed IP");
    }

    private bool SendFromValidIpAddress(bool isProduction)
    {
        var allowedIps = new List<string>
        {
            "91.216.191.181",
            "91.216.191.182",
            "91.216.191.183",
            "91.216.191.184",
            "91.216.191.185",
            "5.252.202.255"
        };

        if (!isProduction)
            allowedIps.Add("::1");

        return allowedIps.Contains(_httpContext.IpAddress);
    }
}