using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.api.Models
{
    [Table("UserReviews")]
    public class UserReview
    {
        public string AppUserId { get; set; }
        public int ReviewId { get; set; }
        public AppUser AppUser { get; set; }
        public Review Review { get; set; }
    }
}
