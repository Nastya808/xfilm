using BLL.DTO;


namespace BLL.Interfaces
{
    public interface IRegistrationService
    {
        public Task<bool> AccountRegistration(UserDTO user);
    }
}
