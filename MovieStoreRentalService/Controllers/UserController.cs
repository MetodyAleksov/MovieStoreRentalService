using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services;
using MovieStoreRentalService.Services.Rentals;
using MovieStoreRentalService.Services.User;

namespace MovieStoreRentalService.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;

        public UserController
            (RoleManager<IdentityRole> roleManager
            , IUserService userService
            , IRentalService rentalService
            , ICartService cartService
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IDistributedCache cache
            , IMemoryCache memoryCache)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _rentalService = rentalService;
            _userService = userService;
            _signInManager = signInManager;
            _cartService = cartService;
            _cache = cache;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Profile()
        {
            ApplicationUser currentUser;

            if (!this._memoryCache.TryGetValue("currentUser", out currentUser))
            {
                currentUser = await _userManager.GetUserAsync(HttpContext.User);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5)); 

                this._memoryCache.Set("currentUser", currentUser, cacheEntryOptions);
            }

            RentalDTO[] rentals = new RentalDTO[0];

            if (!this._memoryCache.TryGetValue("rentals", out rentals))
            {
                try
                {
                    var userCartItems = await _cartService.GetUsersCart(currentUser.Id);
                    rentals = userCartItems.Rentals.Select(r => new RentalDTO()
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
                }
                catch (ArgumentException)
                {
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                this._memoryCache.Set("rentals", rentals, cacheEntryOptions);
            }

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

        public async Task<IActionResult> Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string id)
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        public async Task<IActionResult> AddRentalToCart()
        {
            return View();
        }

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
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Administrator"
        //    });

        //    return Ok();
        //}
    }
}
