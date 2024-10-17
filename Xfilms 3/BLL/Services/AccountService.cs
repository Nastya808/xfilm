using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;


namespace Xfilms.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EncryptionService _encryptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IUnitOfWork unit, EncryptionService encryptionService, IEmailService emailService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unit;
            _emailService = emailService;
            _configuration = configuration;
            _encryptionService = encryptionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendPasswordResetLink(string email)
        {
            var user = await _unitOfWork.User.FindByEmailAsync(email); // Ищем пользователя по email
            if (user == null)
            {
                throw new NullReferenceException("Пользователь с данным email не найден.");
            }

            var resetToken = GenerateResetToken();
            var resetLink = $"{_configuration["AppUrl"]}/reset-password?token={resetToken}&email={email}";

            // Сохраняем токен в сессии
            _httpContextAccessor.HttpContext.Session.SetString("ResetToken", resetToken);
            _httpContextAccessor.HttpContext.Session.SetString("ResetTokenExpiry", DateTime.UtcNow.AddHours(24).ToString());

            // Отправляем email
            await _emailService.SendEmailAsync(email, "Восстановление пароля", $"Пройдите по ссылке для сброса пароля: {resetLink}");
        }

        private string GenerateResetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<bool> ValidateResetToken(string email, string token)
        {
            var sessionToken = _httpContextAccessor.HttpContext.Session.GetString("ResetToken");
            var sessionExpiry = _httpContextAccessor.HttpContext.Session.GetString("ResetTokenExpiry");

            if (sessionToken == null || sessionExpiry == null)
            {
                return false;
            }

            var expiryDate = DateTime.Parse(sessionExpiry);
            if (expiryDate < DateTime.UtcNow || sessionToken != token)
            {
                return false;
            }

            var user = await _unitOfWork.User.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task ResetPassword(string email, string newPassword, string token)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                throw new Exception("Пароль должен содержать как минимум 6 символов.");
            }

            if (await ValidateResetToken(email, token))
            {
                var user = await _unitOfWork.User.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new NullReferenceException("Пользователь не найден.");
                }

                _encryptionService.Pass = newPassword;
                _encryptionService.HashPass();
                user.Pass = _encryptionService.PassDb;
                user.Salt = _encryptionService.SaltDb;

                await _unitOfWork.User.UpdateByIdAsync(user);

                _httpContextAccessor.HttpContext.Session.Remove("ResetToken");
                _httpContextAccessor.HttpContext.Session.Remove("ResetTokenExpiry");
            }
            else
            {
                throw new Exception("Неверный токен или срок его действия истёк.");
            }
        }
    }
}
