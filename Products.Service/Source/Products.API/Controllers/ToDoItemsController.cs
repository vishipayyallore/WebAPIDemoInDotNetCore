using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API.Controllers
{

    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    //http://localhost:6059/api/v1/ToDoItems
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

        /// <summary>
        /// http://localhost:4942/api/v1/todoitems/96a28806-81ca-42b7-a4d3-feb1be00adbc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            return await GetToDoItem(id);
        }

        /// <summary>
        /// http://localhost:4942/api/v2/todoitems/96a28806-81ca-42b7-a4d3-feb1be00adbc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), MapToApiVersion("2.0")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            return await GetToDoItem(id);
        }


        private async Task<IActionResult> GetToDoItem(Guid id)
        {
            var todoItem = await Task.FromResult(_toDoContext.ToDoItems.FirstOrDefault((p) => p.Id == id));
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

    }

}
