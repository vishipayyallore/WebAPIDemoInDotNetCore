using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.Domain;
using System.Collections.Generic;
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

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return null;
        }

    }

}