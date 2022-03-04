using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.Core;

namespace MovieStoreRentalService.Controllers
{
    public class RentalController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string imageUrl, string type, string amountAvailable, string price)
        {
            try
            {
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
