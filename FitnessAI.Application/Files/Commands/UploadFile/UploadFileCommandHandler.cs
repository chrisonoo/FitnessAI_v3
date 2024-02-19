using FitnessAI.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using File = FitnessAI.Domain.Entities.File;

namespace FitnessAI.Application.Files.Commands.UploadFile;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileManagerService _fileManagerService;

    public UploadFileCommandHandler(
        IApplicationDbContext context,
        IFileManagerService fileManagerService)
    {
        _context = context;
        _fileManagerService = fileManagerService;
    }

    public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        await _fileManagerService.Upload(request.Files);

        foreach (var file in request.Files)
        {
            var fileInDb = await _context.Files.FirstOrDefaultAsync(x => x.Name == file.FileName);

            if (fileInDb == null)
                AddFile(file);
            else
                UpdateFile(file, fileInDb);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private void UpdateFile(IFormFile file, File fileInDb)
    {
        fileInDb.Description = file.FileName;
        fileInDb.Bytes = file.Length;
    }

    private void AddFile(IFormFile file)
    {
        var newFile = new File
        {
            Bytes = file.Length,
            Description = file.FileName,
            Name = file.FileName
        };

        _context.Files.Add(newFile);
    }
}