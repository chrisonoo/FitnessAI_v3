using MediatR;

namespace FitnessAI.Application.Employees.Queries.GetEmployeePage;

public class GetEmployeePageQuery : IRequest<EmployeePageDto>
{
    public string Url { get; set; }
}