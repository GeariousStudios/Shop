using Microsoft.EntityFrameworkCore;
using Shop.api.Data;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync(QueryObject query)
        {
            var reviews = _context.Reviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                reviews = reviews.Where(p => p.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    reviews = query.IsDescending
                        ? reviews.OrderByDescending(p => p.Name)
                        : reviews.OrderBy(p => p.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await reviews.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);

            await _context.SaveChangesAsync();

            return reviewModel;
        }

        public async Task<Review?> UpdateAsync(int id, Review reviewModel)
        {
            var existingReview = await _context.Reviews.FindAsync(id);

            if (existingReview == null)
            {
                return null;
            }

            existingReview.Name = reviewModel.Name;
            existingReview.Description = reviewModel.Description;
            existingReview.ImageUrl = reviewModel.ImageUrl;
            existingReview.Score = reviewModel.Score;

            await _context.SaveChangesAsync();

            return existingReview;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (reviewModel == null)
            {
                return null;
            }

            _context.Reviews.Remove(reviewModel);
            await _context.SaveChangesAsync();

            return reviewModel;
        }

        public async Task<bool> ReviewExists(int id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }
    }
}
