using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    internal class UserRepository : IEmailUser
    {
        private readonly XFilmContext _Db;
        public UserRepository(XFilmContext context )=>_Db = context;

        public async Task<bool>  CreateAsync(User entity)
        {

            if (entity != null)
            {
              await _Db.AddAsync(entity);
              await _Db.SaveChangesAsync();
              return true;
            }
            else {

                throw new NullReferenceException("Chek entity");
            }
        }

        public async Task<bool> DleleteByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Check id, id mast by != 0 ");
            }
            var targetDelete=await _Db.Users.SingleOrDefaultAsync(u => u.Id == id);


            if (targetDelete != null)
            {

                _Db.Remove(targetDelete);
                await _Db.SaveChangesAsync();
                return true;

            }
            
            return false;

        }

        public async Task<User> GeByIdAsync(int id)
        {
            if(id == 0)
            {
                throw new ArgumentException("Check id, id mast by != 0 ");
            }
            var targetReturn = await _Db.Users.SingleOrDefaultAsync(u=>u.Id==id);

            if (targetReturn != null)
            {
                return targetReturn;

            }
            else { throw new NullReferenceException("User not found"); }
            

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.Run(()=> _Db.Users);
        }

        public async Task<bool> UpdateByIdAsync(User incommingUser)
        {
            if (incommingUser.Id== 0)
            {
                throw new ArgumentException("Check id, id mast by != 0 ");
            }
            var targetUpdate = await _Db.Users.SingleOrDefaultAsync(u => u.Id == incommingUser.Id);

            
            if (targetUpdate != null)
            {
                targetUpdate.IsAssert=incommingUser.IsAssert;
                targetUpdate.Salt = incommingUser.Salt;
                targetUpdate.Pass = incommingUser.Pass;
                targetUpdate.Email = incommingUser.Email;
                await _Db.SaveChangesAsync();         
                return true;

            }

            return false;
        }
        // Метод для поиска пользователя по email
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _Db.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

       
    }
}
