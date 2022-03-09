using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;
using MovieStoreRentalService.Services.Rentals;

namespace MovieStoreRentalService.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            this._rentalService = rentalService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string imageUrl, string type, int amountAvailable, decimal price)
        {
            try
            {
                Enum.TryParse(type, true, out RentalType rentalType);

                var dto = new RentalDTO()
                {
                    Name = name,
                    ImageURL = imageUrl,
                    RentalType = rentalType,
                    AmountAvailable = amountAvailable,
                    Price = price
                };

                _rentalService.AddRental(dto);

                ViewData[Constants.SuccessMessage] = $"{type} successfully added!";
                return View();
            }
            catch (Exception)
            {
                ViewData[Constants.ErrorMessage] = "Something went wrong!";
                return Redirect("/Rentals/Add");
            }
        }
    }
}
