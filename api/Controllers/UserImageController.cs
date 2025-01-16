using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.api.Extensions;
using Shop.api.Interfaces;
using Shop.api.Models;

namespace Shop.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserImageController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageRepository _imageRepo;
        private readonly IUserImageRepository _userImageRepo;

        public UserImageController(
            UserManager<AppUser> userManager,
            IImageRepository imageRepo,
            IUserImageRepository userImageRepo
        )
        {
            _userManager = userManager;
            _imageRepo = imageRepo;
            _userImageRepo = userImageRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserImage>>> GetUserImage()
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var userImage = await _userImageRepo.GetUserImage(appUser);

            return Ok(userImage);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserImage>>> AddUserImage(int imageId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var image = await _imageRepo.GetByIdAsync(imageId);

            if (image == null)
            {
                return BadRequest("Image not found");
            }

            var userImage = await _userImageRepo.GetUserImage(appUser);

            if (userImage.Any(i => i.Id == imageId))
            {
                return BadRequest("Cannot add same image twice");
            }

            var userImageModel = new UserImage { ImageId = image.Id, AppUserId = appUser.Id };

            await _userImageRepo.CreateAsync(userImageModel);

            if (userImageModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserImage>>> DeleteUserImage(int imageId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);

            var userImage = await _userImageRepo.GetUserImage(appUser);

            var filteredImage = userImage.Where(i => i.Id == imageId).ToList();

            if (filteredImage.Count() == 1)
            {
                await _userImageRepo.DeleteUserImage(appUser, imageId);
            }
            else
            {
                return BadRequest("Image does not exist in this UserImage");
            }

            return Ok("Image removed from this UserImage");
        }
    }
}
