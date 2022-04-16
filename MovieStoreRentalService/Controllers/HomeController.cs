using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger
            , IRentalService rentalService
            , IMemoryCache memoryCache)
        {
            _logger = logger;
            _rentalService = rentalService;
            _memoryCache = memoryCache;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<RentalDTO> rentals;

            if (!this._memoryCache.TryGetValue("date", out rentals)) // Look for cache key.
            {
                rentals = _rentalService
                .ListAllRentals()
                .OrderByDescending(t => t.TimeAdded)
                .Take(3)
                .ToList(); // Key not in cache, so get data.

                var cacheEntryOptions = new MemoryCacheEntryOptions() // Set cache options.
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Keep in cache for this time.
                                                                        // Reset time if accessed.

                // Save data in cache.
                this._memoryCache.Set("date", rentals, cacheEntryOptions);
            }

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