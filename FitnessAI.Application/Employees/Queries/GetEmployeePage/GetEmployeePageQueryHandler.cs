﻿using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Employees.Queries.GetEmployeePage;

public class GetEmployeePageQueryHandler : IRequestHandler<GetEmployeePageQuery, EmployeePageDto>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeePageQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeePageDto> Handle(GetEmployeePageQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Employee)
            .Where(x => x.Employee != null)
            .Select(x => new { x.IsDeleted, Url = x.Employee.WebsiteUrl, Content = x.Employee.WebsiteRaw })
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.Url == request.Url);

        return new EmployeePageDto { Content = employee.Content };
    }
}