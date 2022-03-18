using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStoreRentalService.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
