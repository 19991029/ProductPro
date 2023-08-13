using Microsoft.EntityFrameworkCore;
using ProductPro.Data;
using ProductPro.Migrations;
using ProductPro.Models;
using System.Linq;
using System.Linq.Expressions;

namespace ProductPro.Repository
{
    public class Reposotory<T> : IRepository<T> where T : class
    {
        private readonly ProductDbContext db;
        private DbSet<T> dbSet;


        public Reposotory(ProductDbContext _db)
        {
            db = _db;
            this.dbSet=_db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
           await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;  //AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;  //AsQueryable();
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
           dbSet.Remove(entity);
           await SaveAsync();
        }

        public async Task SaveAsync()
        {
           await db.SaveChangesAsync();
        }
    }
}
