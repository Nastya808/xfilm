namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task SendPasswordResetLink(string email); // Отправка ссылки для восстановления пароля
        Task<bool> ValidateResetToken(string email, string token); // Проверка валидности токена
        Task ResetPassword(string email, string newPassword, string token); // Сброс пароля
    }
}
