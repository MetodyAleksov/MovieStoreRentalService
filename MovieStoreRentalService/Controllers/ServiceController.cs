using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services;
using MovieStoreRentalService.Services.Rentals;

namespace MovieStoreRentalService.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceController(IRentalService rentalService, ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _rentalService = rentalService;
            _cartService = cartService;
            _userManager = userManager; 
        }

        public IActionResult Shop()
        {
            List<RentalDTO> rentals = _rentalService.ListAllRentals().ToList();

            ViewData["Rentals"] = rentals;

            return View();
        }

        public IActionResult IndividualView(string id)
        {
            (bool isValid, RentalDTO dto) = _rentalService.FindById(id);

            if (isValid)
            {
                ViewData["model"] = dto;

                return View();
            }

            return Redirect("/Service/Shop");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart()
        {
            return Redirect("/User/Profile");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string rentalId)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            await _cartService.RemoveRentalFromCart(rentalId, currentUser.Id);
            return Redirect("/User/Profile");
        }
    }
}
