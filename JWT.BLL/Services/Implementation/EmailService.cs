using System.Net;
using System.Net.Mail;
using JWT.BLL.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace JWT.BLL.Services.Implementation;

public class EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, IConfiguration configuration)
    : IEmailService
{
    private readonly string _smtpServer = smtpServer;
    private readonly int _smtpPort = smtpPort;
    private readonly string _smtpUsername = smtpUsername;
    private readonly string _smtpPassword = smtpPassword;
    private IConfiguration Configuration => configuration;

    public async Task SendEmailAsync(string toEmail, string subject, string emailContent)
    {
        var fromEmail = Configuration["Mail"];
        var displayName = Configuration["DisplayName"];
        var password = Configuration["Password"];
        var host = Configuration["Host"];
        var port = int.Parse(Configuration["Port"]);

        using (var client = new SmtpClient(host, port))
        {
            client.Credentials = new NetworkCredential(fromEmail, password);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, displayName),
                Subject = subject,
                Body = emailContent,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
        }
    }
}