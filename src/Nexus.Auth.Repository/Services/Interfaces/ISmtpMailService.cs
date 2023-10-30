using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface ISmtpMailService
{
    Task SendMailAsync(MailData data, bool isHtml = true);
}