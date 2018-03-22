using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Service.Controllers
{

    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ToDoItemsController : Controller
    {
        private readonly ToDoContext _toDoContext;
        private readonly IEnumerable<TodoItem> _todoItems = new[]
        {
            new TodoItem { Name = "Read Articles"},
            new TodoItem { Name = "Watch MSDN Videos"}
        };

        public ToDoItemsController(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
            if (_toDoContext.ToDoItems.Any()) return;
            _toDoContext.ToDoItems.AddRange(_todoItems);
            _toDoContext.ToDoItems.Add(new TodoItem { Name = "Practice Programs" });
            _toDoContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAllToDoItems()
        {
            return await Task.FromResult<IEnumerable<TodoItem>>(_toDoContext.ToDoItems.ToList());
        }

    }

}
