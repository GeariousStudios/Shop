using System.Data.Entity;
using Shop.api.Data;
using Shop.api.Dtos;
using Shop.api.Helpers;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetAllAsync(QueryObject query)
        {
            var images = _context.Images.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                images = images.Where(i => i.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    images = query.IsDescending
                        ? images.OrderByDescending(i => i.CreatedAt)
                        : images.OrderBy(i => i.CreatedAt);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await images.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Image> CreateAsync(Image imageModel)
        {
            await _context.Images.AddAsync(imageModel);
            await _context.SaveChangesAsync();

            return imageModel;
        }

        public async Task<Image?> UpdateAsync(int id, UpdateImageRequestDto imageDto)
        {
            var existingImage = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (existingImage == null)
            {
                return null;
            }

            existingImage.Name = imageDto.Name;
            existingImage.ImageUrl = imageDto.ImageUrl;

            await _context.SaveChangesAsync();

            return existingImage;
        }

        public async Task<Image?> DeleteAsync(int id)
        {
            var imageModel = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (imageModel == null)
            {
                return null;
            }

            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();

            return imageModel;
        }

        public async Task<bool> ImageExists(int id)
        {
            return await _context.Images.AnyAsync(i => i.Id == id);
        }
    }
}
