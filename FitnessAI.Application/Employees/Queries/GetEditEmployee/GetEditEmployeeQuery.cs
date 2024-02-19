using MediatR;

namespace FitnessAI.Application.Employees.Queries.GetEditEmployee;

public class GetEditEmployeeQuery : IRequest<EditEmployeeVm>
{
    public string UserId { get; set; }
}