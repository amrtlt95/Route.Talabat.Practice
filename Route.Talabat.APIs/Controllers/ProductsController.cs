using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> productRepository;

        public ProductsController(IGenericRepository<Product> ProductRepository) => productRepository = ProductRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
                return NotFound(new {StatusCode=404,Message= "Not found"});
            return Ok(product);
        }

    }
}
