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

        /// <summary>
        /// Route: http://localhost:8033/api/products/63a371ea-faca-4ed0-b747-090c7aeddae0
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProductById")]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById([FromRoute]Guid id)
        {
            var product = await Task.FromResult(_productsContext.ProductsSet.FirstOrDefault((p) => p.Id == id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// To Add new product. 
        /// Route: http://localhost:8033/api/products/CreateProduct
        /// Returns location →http://localhost:4942/api/Products/200e5eaa-77d3-4186-aaf1-1be513cc3671 in output
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            await Task.FromResult(_productsContext.ProductsSet.Add(product));
            _productsContext.SaveChanges();

            var currentRoute = $"GetProductById";
            return CreatedAtRoute(
                routeName: currentRoute,
                routeValues: new { id = product.Id },
                value: product);
        }

        /// <summary>
        /// Route: http://localhost:8033/api/products/UpdateProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            var currentProduct = await Task.FromResult(_productsContext.ProductsSet.FirstOrDefault(pduct => pduct.Id == product.Id));
            if (currentProduct == null)
            {
                return NotFound();
            }

            // Update only Price is allowed
            currentProduct.Price = product.Price;

            _productsContext.ProductsSet.Update(currentProduct);
            _productsContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Route: http://localhost:8033/api/products/DeleteProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> Delete([FromBody]Product product)
        {
            var currentProduct = await Task.FromResult(_productsContext.ProductsSet.FirstOrDefault(pduct => pduct.Id == product.Id));
            if (currentProduct == null)
            {
                return NotFound();
            }

            _productsContext.ProductsSet.Remove(currentProduct);
            _productsContext.SaveChanges();

            return NoContent();
        }

    }

}