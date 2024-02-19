using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Users.Extensions;
using MediatR;

namespace FitnessAI.Application.Employees.Queries.GetEmployeeBasics;

public class GetEmployeeBasicsQueryHandler : IRequestHandler<GetEmployeeBasicsQuery, IEnumerable<EmployeeBasicsDto>>
{
    private readonly IUserRoleManagerService _userRoleManagerService;

    public GetEmployeeBasicsQueryHandler(
        IUserRoleManagerService userRoleManagerService)
    {
        _userRoleManagerService = userRoleManagerService;
    }

    public async Task<IEnumerable<EmployeeBasicsDto>> Handle(GetEmployeeBasicsQuery request,
        CancellationToken cancellationToken)
    {
        var employees = (await _userRoleManagerService
                .GetUsersInRoleAsync(RolesDict.PRACOWNIK))
            .Select(x => x.ToEmployeeBasicsDto());

        return employees;
    }
}