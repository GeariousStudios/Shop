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
                Score = reviewModel.Score,
            };
        }

        public static Review ToReviewFromCreateDto(this CreateReviewRequestDto reviewDto)
        {
            return new Review
            {
                Name = reviewDto.Name,
                Description = reviewDto.Description,
                ImageUrl = reviewDto.ImageUrl,
                Score = reviewDto.Score,
            };
        }
    }
}
