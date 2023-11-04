using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace WebAPI.Authentication;

public class CustomEmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;
    private readonly SecurityEmail _configurations;
    
    public CustomEmailSender(IOptions<SecurityConfigurations> options)
    {
        _configurations = options.Value.Email;
        _smtpClient = new()
        {
            Host = _configurations.Host,
            Port = _configurations.Port,
            Credentials = new NetworkCredential(_configurations.Address, _configurations.Password),
            EnableSsl = true,
        };
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage(_configurations.Address, email, subject, message)
        {
            IsBodyHtml = true,
        };

        await _smtpClient.SendMailAsync(mailMessage);
    }
}