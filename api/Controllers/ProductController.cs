using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Mappers;

namespace Shop.api.Models.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productRepo.GetAllAsync(query);

            var productDto = products.Select(p => p.ToProductDto());

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToProductDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> Create(
            [FromBody] CreateProductDto productDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = productDto.ToProductFromCreate();

            await _productRepo.CreateAsync(productModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = productModel.Id },
                productModel.ToProductDto()
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> Update(
            [FromRoute] int id,
            [FromBody] UpdateProductDto updateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = await _productRepo.UpdateAsync(id, updateDto);

            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel.ToProductDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = await _productRepo.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
