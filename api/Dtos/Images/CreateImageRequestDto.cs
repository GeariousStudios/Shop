using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Dtos
{
    public class CreateImageRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 100 characters")]
        [Index(nameof(Name))]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
