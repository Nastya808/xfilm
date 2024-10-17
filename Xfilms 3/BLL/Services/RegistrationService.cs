using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EncryptionService _encryptionService;

        private readonly ILogger<RegistrationService> _logger;
        public RegistrationService(IUnitOfWork unitOfWork, EncryptionService encryption, ILogger<RegistrationService> logger)
        {
            _logger = logger;
            _encryptionService = encryption;
            _unitOfWork = unitOfWork;

        }
        public async Task<bool> AccountRegistration(UserDTO user)
        {
            
            if (user != null)
            {
                _encryptionService.Pass=user.Pass;
                _encryptionService.HashPass();
            }

            _logger.LogInformation($"----->>>>{_encryptionService.PassDb}");

            return await _unitOfWork.User.CreateAsync(new DAL.Entities.User
            {
                Email = user.Email,
                IsAssert=user.IsAssert,
                MaxCountProfile = user.MaxCountProfile,
                Pass=_encryptionService.PassDb!,
                Salt = _encryptionService.SaltDb!,
            });
        }
    }
}
