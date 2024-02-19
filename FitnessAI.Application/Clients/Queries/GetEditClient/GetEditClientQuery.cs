using FitnessAI.Application.Clients.Commands.EditClient;
using MediatR;

namespace FitnessAI.Application.Clients.Queries.GetEditClient;

public class GetEditClientQuery : IRequest<EditClientCommand>
{
    public string UserId { get; set; }
}