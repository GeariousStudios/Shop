using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shop.api.Data;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            return await products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context
                .Products.Include(p => p.Name)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
                return null;

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.ImageUrl = productDto.ImageUrl;
            existingProduct.Price = productDto.Price;
            existingProduct.Stock = productDto.Stock;
            existingProduct.CategoryId = productDto.CategoryId;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (productModel == null)
                return null;

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
