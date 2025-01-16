using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("UserProducts")]
    public class UserProduct
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
