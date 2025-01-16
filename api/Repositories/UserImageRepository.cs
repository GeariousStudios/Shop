using Microsoft.EntityFrameworkCore;
using Shop.api.Data;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Repositories
{
    public class UserImageRepository : IUserImageRepository
    {
        private readonly AppDbContext _context;

        public UserImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetUserImage(AppUser user)
        {
            return await _context
                .UserImages.Where(i => i.AppUserId == user.Id)
                .Select(image => new Image
                {
                    Id = image.ImageId,
                    Name = image.Image.Name,
                    ImageUrl = image.Image.ImageUrl,
                })
                .ToListAsync();
        }
    }
}
