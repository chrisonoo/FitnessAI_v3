using FitnessAI.Application.Files.Queries.GetFiles;
using File = FitnessAI.Domain.Entities.File;

namespace FitnessAI.Application.Files.Extensions;

public static class FileExtensions
{
    public static FileDto ToDto(this File file)
    {
        if (file == null)
            return null;

        return new FileDto
        {
            Name = file.Name,
            Bytes = file.Bytes,
            Description = file.Description,
            Id = file.Id,
            Url = $"/ftp/{file.Name}"
        };
    }
}