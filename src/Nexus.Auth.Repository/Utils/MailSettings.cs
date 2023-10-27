namespace Nexus.Auth.Repository.Utils;

public class MailSettings
{
    public int Port { get; set; }
    public string FromName { get; set; }
    public string EmailFrom { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Smtp { get; set; }
    public bool Ssl { get; set; }
    public string BaseUrl { get; set; }
}