using Microsoft.Extensions.Configuration;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MailKit.Security;

using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class SmtpMailService : ISmtpMailService
{
    private readonly IConfiguration _configuration;

    public SmtpMailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMailAsync(MailData data, bool isHtml)
    {
        var settings = _configuration.GetSection("MailSettings").Get<MailSettings>();
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(settings.FromName, settings.EmailFrom));
            message.To.Add(new MailboxAddress(string.Empty, data.To));
            message.Subject = data.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = data.Body
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(settings.Smtp, settings.Port, SecureSocketOptions.Auto);
                smtpClient.Authenticate(settings.Username, settings.Password);
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }

            await Task.Yield();
        }
        catch
        {
            throw;
        }
    }
}