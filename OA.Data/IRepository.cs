using OA.Core;

namespace OA.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
