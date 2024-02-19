using FitnessAI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FitnessAI.Infrastructure.Services;

public class FileManagerService : IFileManagerService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileManagerService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public void Delete(string name)
    {
        var fileFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "ftp", name);
        File.Delete(fileFullPath);
    }

    public async Task Upload(IEnumerable<IFormFile> files)
    {
        var folderRoot = Path.Combine(_webHostEnvironment.WebRootPath, "ftp");

        if (!Directory.Exists(folderRoot))
            Directory.CreateDirectory(folderRoot);

        if (files == null || !files.Any())
            return;

        foreach (var file in files)
            if (file.Length > 0)
            {
                var filePath = Path.Combine(folderRoot, file.FileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
    }
}