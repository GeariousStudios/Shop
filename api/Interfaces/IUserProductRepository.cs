using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IUserProductRepository
    {
        Task<List<Product>> GetUserProduct(AppUser user);
        Task<UserProduct> CreateAsync(UserProduct userProduct);
        Task<UserProduct> DeleteUserProduct(AppUser appUser, int productId);
    }
}
