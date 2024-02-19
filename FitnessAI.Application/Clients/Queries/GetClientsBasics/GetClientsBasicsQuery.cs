using MediatR;

namespace FitnessAI.Application.Clients.Queries.GetClientsBasics;

public class GetClientsBasicsQuery : IRequest<IEnumerable<ClientBasicsDto>>
{
}