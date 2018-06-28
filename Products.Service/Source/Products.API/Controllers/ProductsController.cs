using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API.Controllers
{

    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {

        // Variables
        private readonly IProductsContext _productsContext;

        public ProductsController(IProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        /// <summary>
        /// http://localhost:XXX/api/products
        /// http://localhost:8033/api/products/GetProducts
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetProducts")]
        [Route("GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult<IEnumerable<Product>>(_productsContext.ProductsSet.ToList());
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await Task.FromResult(_productsContext.ProductsSet.FirstOrDefault((p) => p.Id == id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }

}