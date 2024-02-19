using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using MediatR;

namespace FitnessAI.Application.Contacts.Commands.SendContactEmail;

public class SendContactEmailCommandHandler : IRequestHandler<SendContactEmailCommand>
{
    private readonly IAppSettingsService _appSettings;
    private readonly IBackgroundWorkerQueue _backgroundWorkerQueue;
    private readonly IEmail _email;

    public SendContactEmailCommandHandler(
        IEmail email,
        IAppSettingsService appSettings,
        IBackgroundWorkerQueue backgroundWorkerQueue)
    {
        _email = email;
        _appSettings = appSettings;
        _backgroundWorkerQueue = backgroundWorkerQueue;
    }

    public async Task<Unit> Handle(SendContactEmailCommand request, CancellationToken cancellationToken)
    {
        var body =
            $"Nazwa: {request.Name}.<br /></br >E-mail nadawcy: {request.Email}.<br /><br />Tytuł wiadomości: {request.Title}.<br /><br />Wiadomość: {request.Message}.<br /><br />Wysłano z: FitnessAI.";

        _backgroundWorkerQueue.QueueBackgroundWorkItem(async _ =>
            {
                await _email.SendAsync(
                    $"Wiadomość z FitnessAI: {request.Title}",
                    body,
                    await _appSettings.Get(SettingsDict.ADMIN_EMAIL));
            }, $"Kontakt. E-mail: {request.Email}");

        await Task.CompletedTask;

        return Unit.Value;
    }
}