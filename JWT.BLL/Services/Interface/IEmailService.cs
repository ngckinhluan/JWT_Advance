namespace JWT.BLL.Services.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string emailContent);
}