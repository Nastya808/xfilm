using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Exceptions;

namespace Xfilms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordResetController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public PasswordResetController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST: api/passwordreset/sendlink
        [HttpPost("sendlink")]
        public async Task<IActionResult> SendPasswordResetLink([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email не может быть пустым.");
            }

            // Валидация корректности email
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
            {
                return BadRequest("Некорректный email.");
            }

            try
            {
                await _accountService.SendPasswordResetLink(email);
                return Ok(new { message = "Ссылка для восстановления пароля отправлена на ваш email." });
            }
            catch (UserNotFoundException) // Специфичное исключение для пользователя
            {
                return NotFound(new { message = "Пользователь с данным email не найден." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка: " + ex.Message });
            }
        }

        // POST: api/passwordreset/resetpassword
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] Dictionary<string, string> request)
        {
            if (!request.ContainsKey("email") || !request.ContainsKey("newPassword") || !request.ContainsKey("token") ||
                string.IsNullOrEmpty(request["email"]) || string.IsNullOrEmpty(request["newPassword"]) || string.IsNullOrEmpty(request["token"]))
            {
                return BadRequest("Email, новый пароль и токен не могут быть пустыми.");
            }

            if (request["newPassword"].Length < 6)
            {
                return BadRequest("Пароль должен содержать как минимум 6 символов.");
            }

            try
            {
                await _accountService.ResetPassword(request["email"], request["newPassword"], request["token"]);
                return Ok(new { message = "Пароль успешно обновлен." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Произошла ошибка: {ex.Message}" });
            }
        }
    }
}