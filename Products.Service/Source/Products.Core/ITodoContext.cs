using Microsoft.EntityFrameworkCore;
using Products.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Core
{

    public interface ITodoContext : IDisposable
    {
        DbSet<TodoItem> ToDoItems { get; set; }

        int SaveChanges();
    }

}
