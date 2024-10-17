namespace BLL.Interfaces
{
    public interface IEmailService
    {
        // Отправить письмо
        Task SendEmailAsync(string email, string subject, string message);
    }
}