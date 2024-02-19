using FitnessAI.Application.Settings.Queries.Dtos;
using MediatR;

namespace FitnessAI.Application.Settings.Commands.EditSettings;

public class EditSettingsCommand : IRequest
{
    public List<SettingsPositionDto> Positions { get; set; } = new();
}