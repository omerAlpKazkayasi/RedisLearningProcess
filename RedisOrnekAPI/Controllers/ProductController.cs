using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisExample;
using RedisOrnekAPI.Models;
using RedisOrnekAPI.Repository;
using RedisOrnekAPI.Services;

namespace RedisOrnekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
     

        public ProductController(IProductService productRepository)
        {
            _productService = productRepository;
          
        }

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Ok(await _productService.CreateAsync(product));
        }
    }
}
