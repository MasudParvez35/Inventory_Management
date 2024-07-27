using Microsoft.EntityFrameworkCore;
using OA.Core;

namespace OA.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task InsertAsync(T entity)
        {
            await entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
