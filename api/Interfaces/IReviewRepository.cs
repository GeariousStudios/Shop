using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync(QueryObject query);
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review reviewModel);
        Task<Review?> UpdateAsync(int id, UpdateReviewRequestDto reviewDto);
        Task<Review?> DeleteAsync(int id);
        Task<bool> ReviewExists(int id);
    }
}
