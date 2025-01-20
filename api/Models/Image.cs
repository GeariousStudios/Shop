using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        // AppUser association.
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Navigation property
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
