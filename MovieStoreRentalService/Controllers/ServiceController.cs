using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Models;
using MovieStoreRentalService.Services;
using MovieStoreRentalService.Services.Rentals;

namespace MovieStoreRentalService.Controllers
{
    public class ServiceController : BaseController
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

        [AllowAnonymous]
        public IActionResult Shop(int? pageNumber)
        {
            if(pageNumber is null)
            {
                pageNumber = 1;
            }

            int pageSize = 3;

            var rentals = _rentalService.ListAllRentals();

            ViewData["Rentals"] = rentals;

            return View(PaginatedList<RentalDTO>.CreateAsync(rentals, pageNumber ?? 1, pageSize));
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> RemoveFromCart()
        {
            return Redirect("/User/Profile");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string rentalId)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            await _cartService.RemoveRentalFromCart(rentalId, currentUser.Id);
            return Redirect("/User/Profile");
        }
    }
}
