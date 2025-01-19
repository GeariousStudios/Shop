using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.api.Dtos;
using Shop.api.Extensions;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IReviewRepository _reviewRepo;
        private readonly IProductRepository _productRepo;

        public ReviewController(
            UserManager<AppUser> userManager,
            IReviewRepository reviewRepo,
            IProductRepository productRepo
        )
        {
            _userManager = userManager;
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
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

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> Create(
            [FromRoute] int id,
            CreateReviewDto reviewDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewRepo.GetByIdAsync(id);

            if (review == null)
            {
                if (review == null)
                {
                    return BadRequest("Review does not exists");
                }
                else
                {
                    await _reviewRepo.CreateAsync(review);
                }
            }

            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);

            var reviewModel = reviewDto.ToReviewFromCreate(review.Id);
            reviewModel.AppUserId = appUser.Id;

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
            [FromBody] UpdateReviewDto updateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewRepo.UpdateAsync(id, updateDto.ToReviewFromUpdate(id));

            if (review == null)
            {
                return NotFound("Review not found");
            }

            return Ok(review.ToReviewDto());
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
