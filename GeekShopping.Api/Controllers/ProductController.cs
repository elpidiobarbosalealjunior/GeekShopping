using GeekShopping.Api.Data.ValueObjects;
using GeekShopping.Api.Repository;
using GeekShopping.Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAllWithCategories()
        {
            var products = await _productRepository.FindAllWithCategory();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(int id)
        {
            var product = await _productRepository.FindById(id);
            if (product.ProductId <= 0) return NotFound();
            return Ok(product);
        }

        [Authorize]
        [HttpGet("{id}/category")]
        public async Task<ActionResult<ProductVO>> FindByIdWithCategory(int id)
        {
            var product = await _productRepository.FindByIdWithCategory(id);
            if (product.ProductId <= 0) return NotFound();
            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
        {
            if (productVO is null) return BadRequest();
            var product = await _productRepository.Create(productVO);
            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO is null) return BadRequest();
            var product = await _productRepository.Update(productVO);
            return Ok(product);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id <= 0) return BadRequest();
            var status = await _productRepository.Delete(id);
            if (!status) return BadRequest();            
            return Ok(status);
        }
    }
}
