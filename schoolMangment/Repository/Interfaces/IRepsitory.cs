using schoolMangment.DTO;
using System.Linq.Expressions;

namespace schoolMangment.Repository.Interfaces
{
    public interface IRepsitory<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        



    }
}
