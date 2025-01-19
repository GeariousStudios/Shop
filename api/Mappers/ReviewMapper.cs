using Shop.api.Dtos;
using Shop.api.Models;

namespace Shop.api.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                Id = reviewModel.Id,
                Name = reviewModel.Name,
                Description = reviewModel.Description,
                ImageUrl = reviewModel.ImageUrl,
                ProductId = reviewModel.ProductId,
                Score = reviewModel.Score,
            };
        }

        public static Review ToReviewFromCreate(this CreateReviewDto reviewDto, int productId)
        {
            return new Review
            {
                Name = reviewDto.Name,
                Description = reviewDto.Description,
                ImageUrl = reviewDto.ImageUrl,
                ProductId = productId,
                Score = reviewDto.Score,
            };
        }

        public static Review ToReviewFromUpdate(this UpdateReviewDto reviewDto, int productId)
        {
            return new Review
            {
                Name = reviewDto.Name,
                Description = reviewDto.Description,
                ImageUrl = reviewDto.ImageUrl,
                ProductId = productId,
                Score = reviewDto.Score,
            };
        }
    }
}
