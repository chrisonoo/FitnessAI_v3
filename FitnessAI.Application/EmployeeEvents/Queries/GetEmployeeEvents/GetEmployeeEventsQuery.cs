using MediatR;

namespace FitnessAI.Application.EmployeeEvents.Queries.GetEmployeeEvents;

public class GetEmployeeEventsQuery : IRequest<IEnumerable<EmployeeEventDto>>
{
}