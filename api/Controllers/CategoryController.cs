using Microsoft.AspNetCore.Mvc;
using Shop.api.Data;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Mappers;

namespace Shop.api.Models.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ShopDbContext context, ICategoryRepository categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await _categoryRepo.GetAllAsync(query);

            var categoryDto = categories.Select(c => c.ToCategoryDto());

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Category>>> Create(
            [FromBody] CreateCategoryRequestDto categoryDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryModel = categoryDto.ToCategoryFromCreateDto();

            await _categoryRepo.CreateAsync(categoryModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = categoryModel.Id },
                categoryModel.ToCategoryDto()
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<IEnumerable<Category>>> Update(
            [FromRoute] int id,
            [FromBody] UpdateCategoryRequestDto updateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryModel = await _categoryRepo.UpdateAsync(id, updateDto);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<IEnumerable<Category>>> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryModel = await _categoryRepo.DeleteAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
