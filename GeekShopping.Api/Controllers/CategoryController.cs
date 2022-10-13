using GeekShopping.Api.Data.ValueObjects;
using GeekShopping.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVO>>> FindAll()
        {
            var categories = await _categoryRepository.FindAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVO>> FindById(int id)
        {
            var category = await _categoryRepository.FindById(id);
            if (category.CategoryId <= 0) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryVO>> Create([FromBody] CategoryVO categoryVO)
        {
            if (categoryVO is null) return BadRequest();
            var category = await _categoryRepository.Create(categoryVO);
            return Ok(category);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] CategoryVO categoryVO)
        {
            if (categoryVO is null) return BadRequest();
            var category = await _categoryRepository.Update(categoryVO);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var status = await _categoryRepository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
