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
        public IActionResult AddRental()
        {
            try
            {
                string name = ViewData["Name"].ToString();
                string url = ViewData["ImageUrl"].ToString();
                string type = ViewData["Type"].ToString();
                string amount = ViewData["AmountAvailable"].ToString();
                string price = ViewData["Price"].ToString();

                ViewData[Constants.SuccessMessage] = $"{type} successfully added!";
                return Redirect("/");
            }
            catch (Exception)
            {
                ViewData[Constants.ErrorMessage] = "Something went wrong!";
                return Redirect("/Rentals/Add");
            }
        }
    }
}
