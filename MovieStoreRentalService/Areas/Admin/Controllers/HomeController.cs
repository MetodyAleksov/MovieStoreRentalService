using Microsoft.AspNetCore.Mvc;

namespace MovieStoreRentalService.DTO.ViewModels.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
