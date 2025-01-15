using Microsoft.EntityFrameworkCore;
using Shop.api.Data;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext _context;

        public CategoryRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync(QueryObject query)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(c => c.Name.Contains(query.Name));
            }

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;

            await _context.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryModel == null)
            {
                return null;
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
