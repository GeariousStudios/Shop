using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);

        Task<Product?> GetByIdAsync(int id);

        Task<Product> CreateAsync(Product productModel);

        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);

        Task<Product?> DeleteAsync(int id);

        Task<bool> ProductExists(int id);
    }
}
