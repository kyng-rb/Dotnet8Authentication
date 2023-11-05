using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using WebAPI.Authentication.Configurations;

namespace WebAPI.Authentication.Email;

public class CustomEmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;
    private readonly AuthEmail _configurations;
    
    public CustomEmailSender(IOptions<AuthConfigurations> options)
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