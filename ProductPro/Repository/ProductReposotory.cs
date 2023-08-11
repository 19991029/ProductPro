using Microsoft.EntityFrameworkCore;

using ProductPro.Data;
using ProductPro.Models;
using ProductPro.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace productPro.Repository
{
    public class ProductRepository : IProductReposotory
    {
        private readonly ProductDbContext _db;

        public ProductRepository(ProductDbContext db)
        {
            _db = db;
        }
        public async Task Create(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await Save();

        }

        public async Task<Product> Get(Expression<Func<Product, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Product> query = _db.Products;  //AsQueryable();
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

        public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _db.Products;  //AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(Product entity)
        {
            _db.Products.Remove(entity);
            await Save();

        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Product entity)
        {
            _db.Products.Update(entity);
            await Save();
        }
    }
}
