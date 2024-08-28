using System.Net;
using System.Net.Mail;
using JWT.BLL.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace JWT.BLL.Services.Implementation;

public class EmailService(IConfiguration configuration) : IEmailService
{
    private readonly string? _smtpServer = configuration["MailSettings:Host"];
    private readonly int _smtpPort = int.Parse(configuration["MailSettings:Port"] ?? string.Empty);
    private readonly string? _smtpUsername = configuration["MailSettings:Mail"];
    private readonly string? _smtpPassword = configuration["MailSettings:Password"];

    public async Task SendEmailAsync(string toEmail, string subject, string emailContent)
    {
        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = emailContent,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
        }
    }
}