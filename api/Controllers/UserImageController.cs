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
        private readonly IProductRepository _prodRepo;
        private readonly IUserImageRepository _userImageRepo;

        public UserImageController(
            UserManager<AppUser> userManager,
            IProductRepository prodRepo,
            IUserImageRepository userImageRepo
        )
        {
            _userManager = userManager;
            _prodRepo = prodRepo;
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
    }
}
