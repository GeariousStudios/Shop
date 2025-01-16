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
    public class UserProductController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepo;
        private readonly IUserProductRepository _userProductRepo;

        public UserProductController(
            UserManager<AppUser> userManager,
            IProductRepository productRepo,
            IUserProductRepository userProductRepo
        )
        {
            _userManager = userManager;
            _productRepo = productRepo;
            _userProductRepo = userProductRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserProduct>>> GetUserProduct()
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var userProduct = await _userProductRepo.GetUserProduct(appUser);

            return Ok(userProduct);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserProduct>>> AddUserProduct(int productId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);
            var product = await _productRepo.GetByIdAsync(productId);

            if (product == null)
            {
                return BadRequest("Product not found");
            }

            var userProduct = await _userProductRepo.GetUserProduct(appUser);

            if (userProduct.Any(i => i.Id == productId))
            {
                return BadRequest("Cannot add same product twice");
            }

            var userProductModel = new UserProduct { ProductId = product.Id, AppUserId = appUser.Id };

            await _userProductRepo.CreateAsync(userProductModel);

            if (userProductModel == null)
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
        public async Task<ActionResult<IEnumerable<UserProduct>>> DeleteUserProduct(int productId)
        {
            var email = User.GetEmail();
            var appUser = await _userManager.FindByEmailAsync(email);

            var userProduct = await _userProductRepo.GetUserProduct(appUser);

            var filteredProduct = userProduct.Where(i => i.Id == productId).ToList();

            if (filteredProduct.Count() == 1)
            {
                await _userProductRepo.DeleteUserProduct(appUser, productId);
            }
            else
            {
                return BadRequest("Product does not exist in this UserProduct");
            }

            return Ok("Product removed from this UserProduct");
        }
    }
}
