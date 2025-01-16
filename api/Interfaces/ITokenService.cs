using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
