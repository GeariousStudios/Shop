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

        public async Task<UserImage> CreateAsync(UserImage userImage)
        {
            await _context.UserImages.AddAsync(userImage);
            await _context.SaveChangesAsync();
            return userImage;
        }

        public async Task<UserImage> DeleteUserImage(AppUser appUser, int imageId)
        {
            var userImageModel = await _context.UserImages.FirstOrDefaultAsync(i =>
                i.AppUserId == appUser.Id && i.Image.Id == imageId
            );

            if (userImageModel == null)
            {
                return null;
            }

            _context.UserImages.Remove(userImageModel);
            await _context.SaveChangesAsync();

            return userImageModel;
        }
    }
}
