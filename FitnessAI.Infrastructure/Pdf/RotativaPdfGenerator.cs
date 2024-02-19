using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Common.Models;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace FitnessAI.Infrastructure.Pdf;

public class RotativaPdfGenerator : IPdfFileGenerator
{
    public async Task<byte[]> GetAsync(FileGeneratorParams @params)
    {
        var pdfResult = new ViewAsPdf(@params.ViewTemplate, @params.Model)
        {
            PageSize = Size.A4,
            PageOrientation = Orientation.Portrait
        };

        return await pdfResult.BuildFile(@params.Context);
    }
}