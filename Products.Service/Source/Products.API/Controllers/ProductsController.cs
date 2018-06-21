using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.Domain;
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
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult<IEnumerable<Product>>(_productsContext.ProductsSet.ToList());
        }

    }

}