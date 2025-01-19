using Microsoft.EntityFrameworkCore;
using Shop.api.Data;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class UserReviewRepository : IUserReviewRepository
    {
        private readonly AppDbContext _context;

        public UserReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetUserReview(AppUser user)
        {
            return await _context
                .UserReviews.Where(i => i.AppUserId == user.Id)
                .Select(review => new Review
                {
                    Id = review.ReviewId,
                    Name = review.Review.Name,
                    ImageUrl = review.Review.ImageUrl,
                })
                .ToListAsync();
        }

        public async Task<UserReview> CreateAsync(UserReview userReview)
        {
            await _context.UserReviews.AddAsync(userReview);
            await _context.SaveChangesAsync();
            return userReview;
        }

        public async Task<UserReview> DeleteUserReview(AppUser appUser, int reviewId)
        {
            var userReviewModel = await _context.UserReviews.FirstOrDefaultAsync(i =>
                i.AppUserId == appUser.Id && i.Review.Id == reviewId
            );

            if (userReviewModel == null)
            {
                return null;
            }

            _context.UserReviews.Remove(userReviewModel);
            await _context.SaveChangesAsync();

            return userReviewModel;
        }
    }
}
