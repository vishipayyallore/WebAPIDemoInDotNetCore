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

        public DbSet<Product> ProductsSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Products");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasData(
            //    new Product { Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            //    new Product { Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            //    new Product { Name = "Hammer", Category = "Hardware", Price = 16.99M }
            //    );
        }

    }

}
