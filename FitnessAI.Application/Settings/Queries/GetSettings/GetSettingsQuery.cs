using MediatR;

namespace FitnessAI.Application.Settings.Queries.GetSettings;

public class GetSettingsQuery : IRequest<IList<SettingsDto>>
{
}