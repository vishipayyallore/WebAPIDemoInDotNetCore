using Microsoft.AspNetCore.Mvc;
using Products.Service.Models;
using System.Collections.Generic;

namespace Products.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {

        public IEnumerable<Product> GetAllProducts()
        {
            return null;
        }


    }
}