using MediatR;

namespace FitnessAI.Application.Files.Queries.GetFiles;

public class GetFilesQuery : IRequest<IEnumerable<FileDto>>
{
}