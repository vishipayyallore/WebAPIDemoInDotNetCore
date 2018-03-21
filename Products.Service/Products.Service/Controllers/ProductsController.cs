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

        public ProductsController(ProductsContext productsContext)
        {
            _productsContext = productsContext;
            if (_productsContext.Products.Count() != 0) return;
            _productsContext.Products.AddRange(_products);
            _productsContext.Products.Add(new Product { Name = "Hammer", Category = "Hardware", Price = 16.99M });
            _productsContext.SaveChanges();
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
    }
}