using System.ComponentModel.DataAnnotations;

namespace Shop.api.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
