using Microsoft.EntityFrameworkCore;
using Shop.api.Data;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly AppDbContext _context;

        public UserProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetUserProduct(AppUser user)
        {
            return await _context
                .UserProducts.Where(i => i.AppUserId == user.Id)
                .Select(product => new Product
                {
                    Id = product.ProductId,
                    Name = product.Product.Name,
                    Description = product.Product.Description,
                    ImageUrl = product.Product.ImageUrl,
                    Price = product.Product.Price,
                    Stock = product.Product.Stock,
                    CategoryId = product.Product.CategoryId,
                })
                .ToListAsync();
        }

        public async Task<UserProduct> CreateAsync(UserProduct userProduct)
        {
            await _context.UserProducts.AddAsync(userProduct);
            await _context.SaveChangesAsync();
            return userProduct;
        }

        public async Task<UserProduct> DeleteUserProduct(AppUser appUser, int productId)
        {
            var userProductModel = await _context.UserProducts.FirstOrDefaultAsync(i =>
                i.AppUserId == appUser.Id && i.Product.Id == productId
            );

            if (userProductModel == null)
            {
                return null;
            }

            _context.UserProducts.Remove(userProductModel);
            await _context.SaveChangesAsync();

            return userProductModel;
        }
    }
}
