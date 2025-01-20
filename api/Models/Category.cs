using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // AppUser association.
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Navigation property.
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
