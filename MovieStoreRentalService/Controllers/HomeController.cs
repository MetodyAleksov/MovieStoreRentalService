using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Models;
using MovieStoreRentalService.Services.Rentals;
using System.Diagnostics;

namespace MovieStoreRentalService.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRentalService _rentalService;

        public HomeController(ILogger<HomeController> logger, IRentalService rentalService)
        {
            _logger = logger;
            _rentalService = rentalService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<RentalDTO> rentals = _rentalService
                .ListAllRentals()
                .OrderByDescending(t => t.TimeAdded)
                .Take(3)
                .ToList();

            ViewData["Rentals"] = rentals;

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}