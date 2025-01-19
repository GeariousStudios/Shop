using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Dtos
{
    public class UpdateReviewDto
    {
        [MaxLength(50, ErrorMessage = "Name cannot exceed 100 characters")]
        [Index(nameof(Name))]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Score must be within 1 to 5")]
        public int Score { get; set; }
    }
}
