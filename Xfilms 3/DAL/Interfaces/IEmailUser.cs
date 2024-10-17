using DAL.Entities;


namespace DAL.Interfaces
{
    public interface IEmailUser:ICRUD<User>
    {
        public Task<User> FindByEmailAsync(string email);
    }
}
