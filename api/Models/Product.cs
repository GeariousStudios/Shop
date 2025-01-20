using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Navigation properties.
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        // AppUser association.
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Metadata.
        public bool IsDeleted { get; set; } // If true, the product will be sent to archive.
    }
}
