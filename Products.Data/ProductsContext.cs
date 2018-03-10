using Microsoft.EntityFrameworkCore;
using Products.Domain;

namespace Products.Data
{
    class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.
                // .UseInMemoryDatabase("Products");
        }
    }
}
