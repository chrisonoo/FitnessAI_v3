using MediatR;

namespace FitnessAI.Application.Employees.Queries.GetEmployeeBasics;

public class GetEmployeeBasicsQuery : IRequest<IEnumerable<EmployeeBasicsDto>>
{
}