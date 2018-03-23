using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    // http://localhost:6059/api/Products?api-version=1.0
    public class ProductsController : Controller
    {
        private readonly ProductsContext _productsContext;
        private readonly IEnumerable<Product> _products = new[]
        {
            new Product {  Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product {  Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product {  Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };
        private readonly ToDoContext _toDoContext;
        private readonly IEnumerable<TodoItem> _todoItems = new[]
        {
            new TodoItem { Name = "Read Articles"},
            new TodoItem { Name = "Watch MSDN Videos"}
        };


        public ProductsController(ProductsContext productsContext, ToDoContext toDoContext)
        {
            _productsContext = productsContext;
            if (!_productsContext.Products.Any())
            {
                _productsContext.Products.AddRange(_products);
                _productsContext.Products.Add(new Product { Name = "Hammer", Category = "Hardware", Price = 16.99M });
                _productsContext.SaveChanges();
            }

            _toDoContext = toDoContext;
            if (_toDoContext.ToDoItems.Any()) return;
            _toDoContext.ToDoItems.AddRange(_todoItems);
            _toDoContext.ToDoItems.Add(new TodoItem { Name = "Practice Programs" });
            _toDoContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.FromResult<IEnumerable<Product>>(_productsContext.Products.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await Task.FromResult(_productsContext.Products.FirstOrDefault((p) => p.Id == id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("ToDoItems")]
        // http://localhost:6059/api/Products/ToDoItems?api-version=1.0
        public async Task<IActionResult> GetAllToDoItems()
        {
            return Ok(new
            {
                ToDoItems = await Task.FromResult<IEnumerable<TodoItem>>(_toDoContext.ToDoItems.ToList()),
                Products = await Task.FromResult<IEnumerable<Product>>(_productsContext.Products.ToList())
            });
        }

        [HttpGet("ToDoItems"), MapToApiVersion("2.0")]
        // http://localhost:6059/api/Products/ToDoItems?api-version=2.0
        public async Task<IActionResult> GetAllToDoItemsV2()
        {
            return Ok(new
            {
                Products = await Task.FromResult<IEnumerable<Product>>(_productsContext.Products.ToList()),
                ToDoItems = await Task.FromResult<IEnumerable<TodoItem>>(_toDoContext.ToDoItems.ToList())
            });
        }

    }
}