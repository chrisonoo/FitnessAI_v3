using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Users.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Employees.Queries.GetEditEmployee;

public class GetEditEmployeeQueryHandler : IRequestHandler<GetEditEmployeeQuery, EditEmployeeVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IRoleManagerService _roleManagerService;
    private readonly IUserRoleManagerService _userRoleManagerService;

    public GetEditEmployeeQueryHandler(
        IApplicationDbContext context,
        IRoleManagerService roleManagerService,
        IUserRoleManagerService userRoleManagerService)
    {
        _context = context;
        _roleManagerService = roleManagerService;
        _userRoleManagerService = userRoleManagerService;
    }

    public async Task<EditEmployeeVm> Handle(GetEditEmployeeQuery request, CancellationToken cancellationToken)
    {
        var vm = new EditEmployeeVm();

        vm.Employee = (await _context.Users
                .Include(x => x.Employee)
                .Include(x => x.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.UserId))
            .ToEmployee();

        vm.Employee.RoleIds = (await _userRoleManagerService
                .GetRolesAsync(request.UserId))
            .Select(x => x.Id).ToList();

        vm.AvailableRoles = _roleManagerService.GetRoles().ToList();

        if (!string.IsNullOrWhiteSpace(vm.Employee.ImageUrl))
            vm.EmployeeFullPathImage = $"/ftp/{vm.Employee.ImageUrl}";

        return vm;
    }
}