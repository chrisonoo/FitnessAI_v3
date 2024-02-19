﻿using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using MediatR;

namespace FitnessAI.Application.EmployeeEvents.Commands.DeleteEmployeeEvent;

public class DeleteEmployeeEventCommandHandler : IRequestHandler<DeleteEmployeeEventCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeEventCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployeeEventCommand request, CancellationToken cancellationToken)
    {
        _context.EmployeeEvents.Remove(new EmployeeEvent { Id = request.Id });
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}