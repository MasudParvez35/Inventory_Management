using Microsoft.EntityFrameworkCore;
using OA.Core;

namespace OA.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        #endregion

        #region Ctor
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        #endregion

        #region Methods

        public IQueryable<T> Table => _entities;

        public async Task InsertAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
