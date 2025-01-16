using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("UserImages")]
    public class UserImage
    {
        public string AppUserId { get; set; }
        public int ImageId { get; set; }
        public AppUser AppUser { get; set; }
        public Image Image { get; set; }
    }
}
