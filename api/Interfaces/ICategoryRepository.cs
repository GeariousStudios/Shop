using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Models;

namespace Shop.api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(QueryObject query);

        Task<Category?> GetByIdAsync(int id);

        Task<Category> CreateAsync(Category categoryModel);

        Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto);

        Task<Category?> DeleteAsync(int id);

        Task<bool> CategoryExists(int id);
    }
}
