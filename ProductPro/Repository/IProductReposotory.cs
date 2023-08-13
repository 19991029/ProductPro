using ProductPro.Models;
using ProductPro.Models.Dto;
using System.Linq.Expressions;

namespace ProductPro.Repository
{
    public interface IProductReposotory :IRepository<Product>
    {
      //  Task<List<Product>> GetAll(Expression<Func<Product,bool>> filter=null);
        //Task< Product> Get(Expression<Func<Product,bool>> filter = null, bool tracked = true);
       // Task Create(Product entity);
       // Task Remove(Product entity);
        Task<Product> UpdateAsync(Product entity);
      //  Task Save();


    }
}
