using DAL.EF;
using DAL.Interfaces;


namespace DAL.Repository
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly UserRepository _userRepository=null!;
        //public my Repository _NameRepository

        private readonly XFilmContext _xFilmContext=null!;
        public UnitOfWorkRepository(XFilmContext context)=>_xFilmContext=context;
        public IEmailUser User => new UserRepository(_xFilmContext);
    }
}
