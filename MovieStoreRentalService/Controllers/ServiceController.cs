using Microsoft.AspNetCore.Mvc;

namespace MovieStoreRentalService.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
