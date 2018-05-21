using Microsoft.EntityFrameworkCore;
using Products.Core;
using Products.Domain;

namespace Products.Data
{
    public class ToDoContext : DbContext, ITodoContext
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
