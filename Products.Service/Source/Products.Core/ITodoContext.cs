using Microsoft.EntityFrameworkCore;
using Products.Domain;
using System;

namespace Products.Core
{

    public interface ITodoContext : IDisposable
    {
        DbSet<TodoItem> ToDoItems { get; set; }

        int SaveChanges();
    }

}
