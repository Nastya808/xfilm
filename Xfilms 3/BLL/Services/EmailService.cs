using System.Net;
using System.Net.Mail;
using BLL.Interfaces;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPassword)
        {
            _smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUser, smtpPassword), 
                EnableSsl = true
            };
        }

        // Метод для отправки письма
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("xfilms765@gmail.com"), 
                Subject = subject,
                Body = message,
                IsBodyHtml = true 
            };

            mailMessage.To.Add(email);

            // Отправка email
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}