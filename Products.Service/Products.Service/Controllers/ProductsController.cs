using Microsoft.AspNetCore.Mvc;
using Products.Domain;
using Products.Data;
using System.Collections.Generic;
using System.Linq;

namespace Products.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _productsContext;
        readonly IEnumerable<Product> _products = new[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public ProductsController()
        {
            _productsContext = new ProductsContext();
            _productsContext.Products.AddRange(_products);
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productsContext.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault( (p) => p.Id == id );
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}