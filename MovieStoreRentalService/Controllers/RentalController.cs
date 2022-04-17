using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;
using MovieStoreRentalService.Services.Rentals;
namespace MovieStoreRentalService.Controllers
{
    public class RentalController : BaseController
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            this._rentalService = rentalService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Add(string name, string imageUrl, string type, int amountAvailable, decimal price, string description)
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
                    Price = price,
                    Description = description
                };

                _rentalService.AddRental(dto);

                ViewData[Constants.SuccessMessage] = $"{type} successfully added!";
                return View();
            }
            catch (Exception)
            {
                ViewData[Constants.ErrorMessage] = "Something went wrong!";
                return Redirect("/Rental/Add");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Remove(string id)
        {
            _rentalService.RemoveRental(id);

            ViewData[Constants.SuccessMessage] = "Removed rental!";
            return Redirect("/Service/Shop");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var rental = _rentalService.FindById(id);

            ViewData["rentalId"] = id;
            ViewData["rental"] = rental.Item2;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id, string name, string imageUrl, string rentalType, int amountAvailable, decimal price, string description)
        {
            RentalType.TryParse(rentalType, true, out RentalType rentalTypeEnum);

            RentalDTO editedRental = new RentalDTO()
            {
                Name = name,
                ImageURL = imageUrl,
                RentalType = rentalTypeEnum,
                AmountAvailable = amountAvailable,
                Price = price,
                Description = description
            };

            await _rentalService.EditRental(id, editedRental);

            ViewData[Constants.SuccessMessage] = "Successfully edited rental!";
            return Redirect("/");
        }
    }
}
