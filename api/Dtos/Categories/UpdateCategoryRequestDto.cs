using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Dtos
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 100 characters")]
        [Index(nameof(Name))]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
