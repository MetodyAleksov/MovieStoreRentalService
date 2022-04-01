using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services;
using MovieStoreRentalService.Services.Rentals;
using MovieStoreRentalService.Services.User;

namespace MovieStoreRentalService.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController
            (RoleManager<IdentityRole> roleManager
            , IUserService userService
            , IRentalService rentalService
            , ICartService cartService
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _rentalService = rentalService;
            _userService = userService;
            _signInManager = signInManager;
            _cartService = cartService;
        }

    

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var userCartItems = await _cartService.GetUsersCart(currentUser.Id);
            var rentals = userCartItems.Rentals.Select(r => new RentalDTO()
            {
                Id = r.Id,
                AmountAvailable = r.AmountAvailable,
                Description = r.Description,
                ImageURL = r.ImageURL,
                Name = r.Name,
                Price = r.Price,
                RentalType = r.RentalType,
                TimeAdded = r.TimeAdded,
            })
            .OrderByDescending(r => r.TimeAdded)
            .ToArray();

            ViewBag.User = currentUser;
            ViewBag.Rentals = rentals;

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ManageUsers(string id)
        {
            var users = await _userService.GetAllUsers();

            var user = users.SingleOrDefault(u => u.Id == id);

            await _userManager.AddToRoleAsync(user, "Administrator");

            return Ok(user);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout(string id)
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> AddRentalToCart()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRentalToCart(string rentalId)
        {
            (bool isValid, RentalDTO rental) = _rentalService.FindById(rentalId);
            rental.TimeAdded = DateTime.Now;

            if (!isValid)
            {
                ViewData[Constants.ErrorMessage] = $"Something went wrong!";
            }

            ApplicationUser user = await _userManager.GetUserAsync(this.User);

            CartDTO shoppingCart = null;

            try
            {
                shoppingCart = await _cartService.GetUsersCart(user.Id);
            }
            catch (ArgumentException)
            {
                await _cartService.AddCart(user.Id);
                shoppingCart = await _cartService.GetUsersCart(user.Id);
            }

            if (shoppingCart.Rentals.Select(r => r.Id).Contains(rentalId))
            {
                ViewData[Constants.SuccessMessage] = $"Item already in cart!";
                return Redirect("/Service/Shop");
            }

            try
            {
                await _cartService.AddRentalToCart(rentalId, null, shoppingCart.CartId);
            }
            catch (ArgumentException)
            {
            }

            ViewData[Constants.SuccessMessage] = $"Successfully added {rental.Name} to cart!";
            return Redirect("/Service/Shop");
        }

        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    //await _roleManager.CreateAsync(new IdentityRole()
        //    //{
        //    //    Name = "Administrator"
        //    //});

        //    return Ok();
        //}
    }
}
