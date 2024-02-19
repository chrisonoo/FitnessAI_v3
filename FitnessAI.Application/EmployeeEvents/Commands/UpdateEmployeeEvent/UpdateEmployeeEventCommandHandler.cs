﻿using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.EmployeeEvents.Commands.UpdateEmployeeEvent;

public class UpdateEmployeeEventCommandHandler : IRequestHandler<UpdateEmployeeEventCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeEventCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployeeEventCommand request, CancellationToken cancellationToken)
    {
        var employeeEvent = await _context
            .EmployeeEvents
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (request.IsFullDay.GetValueOrDefault())
            request.End = null;

        employeeEvent.End = request.End;
        employeeEvent.IsFullDay = request.IsFullDay.GetValueOrDefault();
        employeeEvent.Start = request.Start;
        employeeEvent.UserId = request.UserId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}