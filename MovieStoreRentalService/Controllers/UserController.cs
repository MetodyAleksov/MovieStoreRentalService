using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;
using MovieStoreRentalService.Services.User;

namespace MovieStoreRentalService.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController
            (RoleManager<IdentityRole> roleManager
            , IUserService userService
            , UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.User = currentUser;

            return View();
        }

        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> ManageUsers(string id)
        //{
        //    var users = await _userService.GetAllUsers();

        //    var user = users.SingleOrDefault(u => u.Id == id);

        //    await _userManager.AddToRoleAsync(user, "Administrator");

        //    return Ok(user);
        //}

        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    //await _roleManager.CreateAsync(new IdentityRole()
        //    //{
        //    //    Name = "Administrator"
        //    //});

        //    return Ok();
        //}
    }
}
