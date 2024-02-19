using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using MailKit.Net.Smtp;
using MimeKit;

namespace FitnessAI.Infrastructure.Services;

public class Email : IEmail
{
    private string _hostSmtp;
    private int _port;
    private string _senderEmail;
    private string _senderEmailPassword;
    private string _senderLogin;
    private string _senderName;

    public async Task Update(IAppSettingsService appSettingsService)
    {
        _hostSmtp = await appSettingsService.Get(SettingsDict.HOST_SMTP);
        _port = Convert.ToInt32(await appSettingsService.Get(SettingsDict.PORT));
        _senderEmail = await appSettingsService.Get(SettingsDict.SENDER_EMAIL);
        _senderEmailPassword = await appSettingsService.Get(SettingsDict.SENDER_EMAIL_PASSWORD);
        _senderName = await appSettingsService.Get(SettingsDict.SENDER_NAME);
        _senderLogin = await appSettingsService.Get(SettingsDict.SENDER_LOGIN);
    }

    public async Task SendAsync(string subject, string body, string to, string attachmentPath = null)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_senderName, _senderEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var builder = new BodyBuilder();

        builder.HtmlBody = @$"
            <html>
                <head></head>
                <body>
                    <div style=""font-size: 16px; padding: 10px; font-family: Arial; line-height: 1.4;"">
                        {body}
                    </div>
                </body>
            </html>
        ";

        if (!string.IsNullOrEmpty(attachmentPath))
            builder.Attachments.Add(attachmentPath);

        message.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_hostSmtp, _port);

            await client.AuthenticateAsync(!string.IsNullOrWhiteSpace(_senderLogin) ? _senderLogin : _senderEmail,
                _senderEmailPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}