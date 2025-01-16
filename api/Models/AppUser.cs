using Microsoft.AspNetCore.Identity;

namespace Shop.api.Models
{
    public class AppUser : IdentityUser
    {
        public override string? UserName
        {
            get => Email;
        }
    }
}
