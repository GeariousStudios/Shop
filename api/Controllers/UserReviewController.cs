using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.api.Extensions;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserReviewController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReviewRepository _reviewRepo;
        private readonly IUserReviewRepository _userReviewRepo;

        public UserReviewController(
            UserManager<AppUser> userManager,
            IReviewRepository reviewRepo,
            IUserReviewRepository userReviewRepo
        )
        {
            _userManager = userManager;
            _reviewRepo = reviewRepo;
            _userReviewRepo = userReviewRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserReview>>> GetUserReview()
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var userReview = await _userReviewRepo.GetUserReview(appUser);

            return Ok(userReview);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserReview>>> AddUserReview(int reviewId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var review = await _reviewRepo.GetByIdAsync(reviewId);

            if (review == null)
            {
                return BadRequest("Review not found");
            }

            var userReview = await _userReviewRepo.GetUserReview(appUser);

            if (userReview.Any(i => i.Id == reviewId))
            {
                return BadRequest("Cannot add same review twice");
            }

            var userReviewModel = new UserReview { ReviewId = review.Id, AppUserId = appUser.Id };

            await _userReviewRepo.CreateAsync(userReviewModel);

            if (userReviewModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserReview>>> DeleteUserReview(int reviewId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);

            var userReview = await _userReviewRepo.GetUserReview(appUser);

            var filteredReview = userReview.Where(i => i.Id == reviewId).ToList();

            if (filteredReview.Count() == 1)
            {
                await _userReviewRepo.DeleteUserReview(appUser, reviewId);
            }
            else
            {
                return BadRequest("Review does not exist in this UserReview");
            }

            return Ok("Review removed from this UserReview");
        }
    }
}
