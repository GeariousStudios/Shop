using Microsoft.AspNetCore.Identity;

namespace Shop.api.Models
{
    public class AppUser : IdentityUser
    {
        public override string? UserName
        {
            get => Email;
        }

        public List<UserImage> UserImages { get; set; } = new List<UserImage>();
        public List<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
    }
}
