using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IUserImageRepository
    {
        Task<List<Image>> GetUserImage(AppUser user);
    }
}
