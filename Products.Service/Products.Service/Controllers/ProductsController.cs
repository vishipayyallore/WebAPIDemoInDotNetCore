using Microsoft.AspNetCore.Mvc;
using Products.Domain;
using Products.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductsContext _productsContext = new ProductsContext();
        readonly IEnumerable<Product> _products = new[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public ProductsController()
        {
            if (_productsContext.Products.Count() != 0) return;
            _productsContext.Products.Add(new Product { Id = 4, Name = "Hammer", Category = "Hardware", Price = 16.99M });
            _productsContext.Products.AddRange(_products);
            _productsContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.FromResult<IEnumerable<Product>>(_productsContext.Products.ToList());
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [Route("GetProduct")]
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