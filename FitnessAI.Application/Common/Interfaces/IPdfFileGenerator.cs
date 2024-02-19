using FitnessAI.Application.Common.Models;

namespace FitnessAI.Application.Common.Interfaces;

public interface IPdfFileGenerator
{
    Task<byte[]> GetAsync(FileGeneratorParams @params);
}