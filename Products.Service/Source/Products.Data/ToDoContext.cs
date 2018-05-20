using Microsoft.EntityFrameworkCore;
using Products.Domain;

namespace Products.Data
{
    public class ToDoContext : DbContext
    {

        public ToDoContext() : base(new DbContextOptions<ProductsContext>())
        {
        }

        public DbSet<TodoItem> ToDoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ToDoItems");
        }
    }
}
