using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services.Rentals;

namespace MovieStoreRentalService.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IRentalService _rentalService;

        public ServiceController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public IActionResult Shop()
        {
            List<RentalDTO> rentals = _rentalService.ListAllRentals().ToList();

            ViewData["Rentals"] = rentals;

            return View();
        }
    }
}
