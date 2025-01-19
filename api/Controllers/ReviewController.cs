using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ReviewController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(UserManager<AppUser> userManager, IReviewRepository reviewRepo)
        {
            _usermanager = userManager;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviews = await _reviewRepo.GetAllAsync(query);

            var reviewDto = reviews.Select(r => r.ToReviewDto());

            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewRepo.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> Create(
            [FromBody] CreateReviewRequestDto reviewDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewModel = reviewDto.ToReviewFromCreateDto();

            await _reviewRepo.CreateAsync(reviewModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = reviewModel.Id },
                reviewModel.ToReviewDto()
            );
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> Update(
            [FromRoute] int id,
            [FromBody] UpdateReviewRequestDto updateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewModel = await _reviewRepo.UpdateAsync(id, updateDto);

            if (reviewModel == null)
            {
                return NotFound();
            }

            return Ok(reviewModel.ToReviewDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewModel = await _reviewRepo.DeleteAsync(id);

            if (reviewModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
