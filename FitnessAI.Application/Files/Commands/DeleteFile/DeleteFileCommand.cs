using MediatR;

namespace FitnessAI.Application.Files.Commands.DeleteFile;

public class DeleteFileCommand : IRequest
{
    public int Id { get; set; }
}