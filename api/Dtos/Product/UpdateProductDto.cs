using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Dtos
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 100 characters")]
        [Index(nameof(Name))]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        public int? ImageUrlId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number")]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
