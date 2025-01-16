using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Mappers;
using Shop.api.Models;

namespace Shop.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepo;

        public ImageController(IImageRepository imageRepo)
        {
            _imageRepo = imageRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Image>>> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var images = await _imageRepo.GetAllAsync(query);

            var imageDto = images.Select(i => i.ToImageDto());

            return Ok(images);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Image>>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _imageRepo.GetByIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image.ToImageDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Image>>> Create(
            [FromBody] CreateImageRequestDto imageDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageModel = imageDto.ToImageFromCreateDto();

            await _imageRepo.CreateAsync(imageModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = imageModel.Id },
                imageModel.ToImageDto()
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Image>>> Update(
            [FromRoute] int id,
            [FromBody] UpdateImageRequestDto updateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageModel = await _imageRepo.UpdateAsync(id, updateDto);

            if (imageModel == null)
            {
                return NotFound();
            }

            return Ok(imageModel.ToImageDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Image>>> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageModel = await _imageRepo.DeleteAsync(id);

            if (imageModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
