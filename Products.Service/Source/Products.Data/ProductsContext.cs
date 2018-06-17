using Microsoft.EntityFrameworkCore;
using Products.Core;
using Products.Domain;

namespace Products.Data
{
    public class ProductsContext : DbContext, IProductsContext
    {

        public ProductsContext() : base(new DbContextOptions<ProductsContext>())
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Products");
        }
    }
}
