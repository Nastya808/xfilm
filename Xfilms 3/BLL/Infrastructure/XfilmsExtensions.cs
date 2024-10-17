using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xfilms.Services;

namespace BLL.Infrastructure
{
    public static class XfilmsExtensions
    {
        public static void XfilmsServise(this IServiceCollection services)
        {
            // Регистрируем UnitOfWork и другие сервисы
            services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAccountService, AccountService>();

            // Регистрация EncryptionService
            services.AddScoped<EncryptionService>();

            // Регистрация EmailService как Scoped
            services.AddScoped<IEmailService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var smtpSettings = configuration.GetSection("SmtpSettings");

                return new EmailService(
                    smtpSettings["Host"],
                    int.Parse(smtpSettings["Port"]),
                    smtpSettings["User"],
                    smtpSettings["Password"]
                );
            });
        }
    }
}
