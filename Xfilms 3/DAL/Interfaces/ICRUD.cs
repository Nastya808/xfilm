

using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICRUD <T> where T : class  
    {
        //Get By Id
        /// <summary>
        /// Get  T  in data base by id  
        /// </summary>
        /// <param name="Id">Only int</param>
        /// <returns>Task T</returns>
        public Task<T> GeByIdAsync(int Id);
        //DeleteBYId

        /// <summary>
        /// Delete  T in data base by id  
        /// </summary>
        /// <param name="id">Only int</param>
        /// <returns>Task bool</returns>
        public Task<bool> DleleteByIdAsync(int id);
        //UpdateById
        /// <summary>
        /// UpdatById T in data base
        /// </summary>
        /// <param name="incomming">T entity</param>
        /// <returns>Task bool</returns>
        public Task<bool> UpdateByIdAsync(T incomming);
        //Create;
        /// <summary>
        /// Create T in Data base
        /// </summary>
        /// <param name="entity">Entity T</param>
        /// <returns>Task bool</returns>
        public Task<bool> CreateAsync(T entity);
        //GetAll
        /// <summary>
        /// Get T colections
        /// </summary>
        /// <returns>Task <IEnumerable<T>> </returns>
        public Task<IEnumerable<T>> GetAllAsync();

    }
}
