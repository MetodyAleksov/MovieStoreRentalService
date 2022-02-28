using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;

namespace MovieStoreRentalService.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Library()
        {
            RentalDTO dto = new RentalDTO()
            {
                Name = "Fast and furious",
                AmountAvailable = 3,
                Price = (decimal)12.02,
                RentalType = RentalType.Movie
            };

            return View(null, dto);
        }
    }
}
