using System.Net;
using System.Net.Mail;
using JWT.BLL.Services.Interface;

namespace JWT.BLL.Services.Implementation;

public class EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    : IEmailService
{
    private readonly string _smtpServer = smtpServer;
    private readonly int _smtpPort = smtpPort;
    private readonly string _smtpUsername = smtpUsername;
    private readonly string _smtpPassword = smtpPassword;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpUsername),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);
        using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}