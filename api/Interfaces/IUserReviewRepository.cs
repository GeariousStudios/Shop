using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IUserReviewRepository
    {
        Task<List<Review>> GetUserReview(AppUser user);
        Task<UserReview> CreateAsync(UserReview userReview);
        Task<UserReview> DeleteUserReview(AppUser appUser, int reviewId);
    }
}
