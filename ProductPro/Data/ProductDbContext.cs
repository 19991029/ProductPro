using Microsoft.EntityFrameworkCore;
using ProductPro.Models;
using ProductPro.Models.Dto;

namespace ProductPro.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext>options):base(options) 
        {

            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1,Name="xyz",Detail="abc",Qty=1 },
                 new Product { Id = 2, Name = "xyz1", Detail = "abc1", Qty = 1 },
                  new Product { Id = 3, Name = "xyz2", Detail = "abc2", Qty = 1 }
                );
        }

    }
}
